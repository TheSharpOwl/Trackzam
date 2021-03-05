from rest_framework.decorators import api_view
from django.http.response import JsonResponse
from rest_framework.response import Response
from rest_framework import status
from django.contrib.auth import authenticate
from django.views.decorators.csrf import csrf_exempt
import base64

from API.models import VideoFile, AudioFile
from VideoAnalyser.detection import split_video, gen_states

@api_view(['POST'])
def send_video_file(request):
    print("In send", end='\n')
    file = request.FILES['file']
    newFile = VideoFile.create(file)
    newFile.save(using='default')

    filename =newFile.file.name.split('/')[-1]
    amount = split_video("VideoServer/VideoFiles/",filename)
    print(amount)
    gen_states(filename.split(".")[0]+".txt", amount)

    return Response({'message': 'File is uploaded'}, status=status.HTTP_201_CREATED)


@api_view(['POST'])
def send_audio_file(request):
    print("In send", end='\n')
    file = request.FILES['file']
    newFile = AudioFile.create(file)
    newFile.save(using='default')
    return Response({'message': 'File is uploaded'}, status=status.HTTP_201_CREATED)


@api_view(['POST', 'GET'])
@csrf_exempt
def authorisation_test(request):
    print("Authorisation")

    if 'HTTP_AUTHORIZATION' in request.META:
        auth = request.META['HTTP_AUTHORIZATION'].split()
        if len(auth) == 2:
            if auth[0].lower() == "basic":
                username, password = base64.b64decode(auth[1]).split(b':')
                username, password = username.decode("utf-8") , password.decode("utf-8")
                print(username,password)
                user = authenticate(username=username, password=password)
                if user is not None:
                    print("aaa")
                    request.user = user
                    print("ok", username, password)
                    return Response({'message': 'Authorized'}, status=status.HTTP_201_CREATED)
                else:
                    print("bbb")
                    print("Not authorized")
                    return Response({'message': 'Not authorized'}, status=status.HTTP_401_UNAUTHORIZED)

    return Response({'message': 'Not authorized'}, status=status.HTTP_401_UNAUTHORIZED)

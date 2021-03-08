from rest_framework.decorators import api_view
from django.http.response import JsonResponse
from rest_framework.response import Response
from rest_framework import status
from django.contrib.auth import authenticate
from django.views.decorators.csrf import csrf_exempt
import base64
from rest_framework.authentication import BasicAuthentication
from rest_framework.permissions import IsAuthenticated
from rest_framework.decorators import permission_classes
from rest_framework.decorators import authentication_classes


from API.models import VideoFile, AudioFile
from VideoAnalyser.detection import split_video, gen_states

@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
def send_video_file(request):
    user = request.user
    print("In send", end='\n')
    file = request.FILES['file']
    newFile = VideoFile.create(file)
    newFile.save(using='default')

    filename = newFile.file.name.split('/')[-1]
    amount = split_video(user.username, filename)
    gen_states(user.username, filename.split(".")[0]+".txt", amount)

    return Response({'message': 'File is uploaded'}, status=status.HTTP_201_CREATED)

@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
def send_audio_file(request):
    user = request.user
    auth = request.auth
    print("In send", end='\n')
    file = request.FILES['file']
    newFile = AudioFile.create(file)
    newFile.save(using='default')
    return Response({'message': 'File is uploaded'}, status=status.HTTP_201_CREATED)


@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
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
                    print(user.is_superuser)
                    print("ok", username, password)
                    return Response({'message': 'Authorized'}, status=status.HTTP_201_CREATED)
                else:
                    print("bbb")
                    print("Not authorized")
                    return Response({'message': 'Not authorized'}, status=status.HTTP_401_UNAUTHORIZED)

    return Response({'message': 'Not authorized'}, status=status.HTTP_401_UNAUTHORIZED)



@api_view(['POST', 'GET'])
@csrf_exempt
def authorisation_user(request):
    print("Authorisation")

    if 'HTTP_AUTHORIZATION' in request.META:
        auth = request.META['HTTP_AUTHORIZATION'].split()
        if len(auth) == 2:
            if auth[0].lower() == "basic":
                try:
                    username, password = base64.b64decode(auth[1]).split(b':')
                    username, password = username.decode("utf-8") , password.decode("utf-8")
                    user = authenticate(username=username, password=password)
                    if user is not None:
                        #request.user = user
                        return True
                    else:
                        print("Not authorized")
                        return False
                except:
                    print("Not authorized")
                    return False
            else:
                print("Not authorized")
                return False
        else:
            print("Not authorized")
            return False
    else:
        print("Not authorized")
        return False


@api_view(['POST', 'GET'])
@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
def test(request):
    user = request.user
    auth = request.auth
    return Response({'message': 'Not authorized'}, status=status.HTTP_401_UNAUTHORIZED)

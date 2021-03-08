from rest_framework.decorators import api_view
from django.http.response import JsonResponse
from rest_framework.response import Response
from rest_framework import status
from django.views.decorators.csrf import csrf_exempt
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
    print("In send", end='\n')
    file = request.FILES['file']
    newFile = AudioFile.create(user.username, file)
    newFile.save(using='default')
    return Response({'message': 'File is uploaded'}, status=status.HTTP_201_CREATED)


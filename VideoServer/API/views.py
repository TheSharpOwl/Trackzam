from rest_framework.decorators import api_view
from django.http.response import JsonResponse
from rest_framework.response import Response
from rest_framework import status
from API.models import VideoFile, AudioFile

@api_view(['POST'])
def send_video_file(request):
    print("In send", end='\n')
    file = request.FILES['file']
    newFile = VideoFile.create(file)
    newFile.save(using='default')
    return Response({'message': 'File is uploaded'}, status=status.HTTP_201_CREATED)


@api_view(['POST'])
def send_audio_file(request):
    print("In send", end='\n')
    file = request.FILES['file']
    newFile = AudioFile.create(file)
    newFile.save(using='default')
    return Response({'message': 'File is uploaded'}, status=status.HTTP_201_CREATED)

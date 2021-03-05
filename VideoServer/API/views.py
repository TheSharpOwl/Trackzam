from rest_framework.decorators import api_view
from django.http.response import JsonResponse
from rest_framework.response import Response
from rest_framework import status
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

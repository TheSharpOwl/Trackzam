from rest_framework.decorators import api_view
from django.http.response import JsonResponse
from rest_framework.response import Response
from rest_framework import status
import requests
import json
import os
import datetime
from requests.auth import HTTPBasicAuth

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
    email = request.query_params['email']
    print(email)
    code = request.META['HTTP_AUTHORIZATION']

    print("In send", end='\n')
    file = request.FILES['file']
    newFile = VideoFile.create(file)
    newFile.save(using='default')

    stamp = request.query_params["start_time"]
    print(stamp)

    filename = newFile.file.name.split('/')[-1]
    amount = split_video(email, filename)
    loc_of_log_file = gen_states(email, filename.split(".")[0]+".txt", amount, stamp)

    with open(loc_of_log_file, 'rb') as f:

        DJANGO_SU_NAME = os.environ.get('DJANGO_SU_NAME')
        DJANGO_SU_PASSWORD = os.environ.get('DJANGO_SU_PASSWORD')

        url = "http://webserver:"+os.environ.get('Server_port')+'/api/send_video_logs'
        parameters = {'email':email}
        #print(DJANGO_SU_NAME, DJANGO_SU_PASSWORD, url)
        r = requests.post(url, files = {'file': f}, params=parameters
                          , auth=HTTPBasicAuth(DJANGO_SU_NAME, DJANGO_SU_PASSWORD),
                          headers={'enctype': "multipart/form-data"})
        print(r.request.body)

    return Response({'message': 'File is uploaded'}, status=status.HTTP_201_CREATED)

@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
def send_audio_file(request):
    email = request.query_params['email']
    print("In send", end='\n')
    file = request.FILES['file']
    newFile = AudioFile.create(file)
    newFile.save(using='default')
    return Response({'message': 'File is uploaded'}, status=status.HTTP_201_CREATED)


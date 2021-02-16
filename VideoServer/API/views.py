from rest_framework.decorators import api_view
from django.http.response import JsonResponse
from rest_framework.response import Response
from rest_framework import status

@api_view(['POST'])
def send_audio_logs(request):
    print("In send", end='\n')
    file = request.FILES['file']
    # TODO
    return Response({'message': 'File is uploaded'}, status=status.HTTP_201_CREATED)
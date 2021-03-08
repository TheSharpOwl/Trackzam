from rest_framework.decorators import api_view
from django.http.response import JsonResponse
from rest_framework.response import Response
from rest_framework import status

from django.views.decorators.csrf import csrf_exempt
from rest_framework.authentication import BasicAuthentication
from rest_framework.permissions import IsAuthenticated
from rest_framework.decorators import permission_classes
from rest_framework.decorators import authentication_classes
from django.contrib.auth.decorators import user_passes_test


from API.parser import parse_into_list
from API.models import AudioRecord, KeyboardRecord
from API.models import MouseRecord, WindowRecord, VideoRecord
from API.serializers import AudioRecordSerializer
from API.serializers import KeyboardRecordSerializer
from API.serializers import MouseRecordSerializer
from API.serializers import WindowRecordSerializer


@api_view(['GET', 'DELETE'])
def not_implemented(request):
    return JsonResponse({'message': 'Not implemented yet.'})


# Audio
@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
def send_audio_logs(request):
    user = request.user
    print("In send", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = AudioRecord.create(user, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Audio logs uploaded'}, status=status.HTTP_201_CREATED)


@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET'])
@user_passes_test(lambda u: u.is_superuser)
def show_audio(request):
    user = request.user
    records = AudioRecord.objects.using('default').all()
    records_serializer = AudioRecordSerializer(records, many=True)
    return Response(records_serializer.data, status=status.HTTP_200_OK)

# Keyboard
@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
def send_keyboard_logs(request):
    user = request.user
    print("In send", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = KeyboardRecord.create(user, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Keyboard logs uploaded'}, status=status.HTTP_201_CREATED)

@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET'])
@user_passes_test(lambda u: u.is_superuser)
def show_keyboard(request):
    user = request.user
    records = KeyboardRecord.objects.using('default').all()
    records_serializer = KeyboardRecordSerializer(records, many=True)
    return Response(records_serializer.data, status=status.HTTP_200_OK)


# Mouse
@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
@user_passes_test(lambda u: u.is_superuser)
def send_mouse_logs(request):
    user = request.user
    print("In send", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = MouseRecord.create(user, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Mouse logs uploaded'}, status=status.HTTP_201_CREATED)


@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET'])
@user_passes_test(lambda u: u.is_superuser)
def show_mouse(request):
    user = request.user
    records = MouseRecord.objects.using('default').all()
    records_serializer = MouseRecordSerializer(records, many=True)
    return Response(records_serializer.data, status=status.HTTP_200_OK)


# Window
@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
def send_window_logs(request):
    user = request.user
    print("In send", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = WindowRecord.create(user, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Window logs uploaded'}, status=status.HTTP_201_CREATED)


@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET'])
@user_passes_test(lambda u: u.is_superuser)
def show_window(request):
    user = request.user
    records = WindowRecord.objects.using('default').all()
    records_serializer = WindowRecordSerializer(records, many=True)
    return Response(records_serializer.data, status=status.HTTP_200_OK)


@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
@user_passes_test(lambda u: u.is_superuser)
def send_video_logs(request):
    user = request.user
    print("In send", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = VideoRecord.create(user, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Video logs uploaded'}, status=status.HTTP_201_CREATED)
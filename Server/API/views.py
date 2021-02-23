from rest_framework.decorators import api_view
from django.http.response import JsonResponse
from rest_framework.response import Response
from rest_framework import status
from API.parser import parse_into_list
from API.models import AudioRecord, KeyboardRecord
from API.models import MouseRecord, WindowRecord
from API.serializers import AudioRecordSerializer
from API.serializers import KeyboardRecordSerializer
from API.serializers import MouseRecordSerializer
from API.serializers import WindowRecordSerializer


@api_view(['GET', 'DELETE'])
def not_implemented(request):
    return JsonResponse({'message': 'Not implemented yet.'})


# Audio
@api_view(['POST'])
def send_audio_logs(request):
    print("In send", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = AudioRecord.create(0, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Audio logs uploaded'}, status=status.HTTP_201_CREATED)

@api_view(['GET'])
def show_audio(request):
    records = AudioRecord.objects.using('default').all()
    records_serializer = AudioRecordSerializer(records, many=True)
    print(records_serializer.data)
    return Response(records_serializer.data, status=status.HTTP_200_OK)

# Keyboard
@api_view(['POST'])
def send_keyboard_logs(request):
    print("In send", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = KeyboardRecord.create(0, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Keyboard logs uploaded'}, status=status.HTTP_201_CREATED)

@api_view(['GET'])
def show_keyboard(request):
    records = KeyboardRecord.objects.using('default').all()
    records_serializer = KeyboardRecordSerializer(records, many=True)
    print(records_serializer.data)
    return Response(records_serializer.data, status=status.HTTP_200_OK)


# Mouse
@api_view(['POST'])
def send_mouse_logs(request):
    print("In send", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = MouseRecord.create(0, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Mouse logs uploaded'}, status=status.HTTP_201_CREATED)

@api_view(['GET'])
def show_mouse(request):
    records = MouseRecord.objects.using('default').all()
    records_serializer = MouseRecordSerializer(records, many=True)
    print(records_serializer.data)
    return Response(records_serializer.data, status=status.HTTP_200_OK)


# Window
@api_view(['POST'])
def send_window_logs(request):
    print("In send", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = WindowRecord.create(0, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Window logs uploaded'}, status=status.HTTP_201_CREATED)

@api_view(['GET'])
def show_window(request):
    records = WindowRecord.objects.using('default').all()
    records_serializer = WindowRecordSerializer(records, many=True)
    print(records_serializer.data)
    return Response(records_serializer.data, status=status.HTTP_200_OK)

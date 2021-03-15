from rest_framework.decorators import api_view
from django.http.response import JsonResponse
from rest_framework.response import Response
from rest_framework import status
import datetime

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
from API.serializers import VideoRecordSerializer
from API.state_processor import generate

# Audio
@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
def send_audio_logs(request):
    email = request.query_params['email']
    print(email)
    print("Processing the request", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = AudioRecord.create(email, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Audio logs uploaded'}, status=status.HTTP_201_CREATED)


@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET'])
@user_passes_test(lambda u: u.is_superuser)
def show_audio(request):
    records = AudioRecord.objects.using('default').all()
    records_serializer = AudioRecordSerializer(records, many=True)
    return Response(records_serializer.data, status=status.HTTP_200_OK)

# Keyboard
@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
def send_keyboard_logs(request):
    email = request.query_params['email']
    print(email)
    print("Processing the request", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = KeyboardRecord.create(email, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Keyboard logs uploaded'}, status=status.HTTP_201_CREATED)

@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET'])
@user_passes_test(lambda u: u.is_superuser)
def show_keyboard(request):
    records = KeyboardRecord.objects.using('default').all()
    records_serializer = KeyboardRecordSerializer(records, many=True)
    return Response(records_serializer.data, status=status.HTTP_200_OK)


# Mouse
@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
def send_mouse_logs(request):
    email = request.query_params['email']
    print(email)
    print("Processing the request", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = MouseRecord.create(email, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Mouse logs uploaded'}, status=status.HTTP_201_CREATED)


@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET'])
@user_passes_test(lambda u: u.is_superuser)
def show_mouse(request):
    records = MouseRecord.objects.using('default').all()
    records_serializer = MouseRecordSerializer(records, many=True)
    return Response(records_serializer.data, status=status.HTTP_200_OK)


# Window
@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
def send_window_logs(request):
    email = request.query_params['email']
    print(email)
    print("Processing the request", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = WindowRecord.create(email, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Window logs uploaded'}, status=status.HTTP_201_CREATED)


@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET'])
@user_passes_test(lambda u: u.is_superuser)
def show_window(request):
    records = WindowRecord.objects.using('default').all()
    records_serializer = WindowRecordSerializer(records, many=True)
    return Response(records_serializer.data, status=status.HTTP_200_OK)


@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['POST'])
#@user_passes_test(lambda u: u.is_superuser)
def send_video_logs(request):
    email = request.query_params['email']
    print(email)
    print("Processing the request", end='\n')
    file = request.FILES['file']
    list = parse_into_list(file)
    for record in list:
        newRecord = VideoRecord.create(email, record['date'], record['time'], record['state'])
        newRecord.save()
    return Response({'message': 'Video logs uploaded'}, status=status.HTTP_201_CREATED)


@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET'])
@user_passes_test(lambda u: u.is_superuser)
def show_video(request):
    records = VideoRecord.objects.using('default').all()
    records_serializer = VideoRecordSerializer(records, many=True)
    return Response(records_serializer.data, status=status.HTTP_200_OK)


@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET', 'POST'])
def check_user(request):
    print("Processing the request", end='\n')
    return Response({'message': 'Credentials are correct.'}, status=status.HTTP_200_OK)



@csrf_exempt
@authentication_classes([BasicAuthentication])
@permission_classes([IsAuthenticated])
@api_view(['GET'])
@user_passes_test(lambda u: u.is_superuser)
def get_suspicious(request):
    print("Processing the request", end='\n')
    start_day = request.query_params["start_day"]
    end_day = request.query_params["end_day"]
    email = request.query_params["email"]


    start_date_split = start_day.split("/")
    start_date = start_date_split[2]+'-'+start_date_split[1]+'-'+start_date_split[0]

    end_date_split = end_day.split("/")
    end_date = end_date_split[2]+'-'+end_date_split[1]+'-'+end_date_split[0]


    records1 = MouseRecord.objects.using('default').filter(username=email).filter(
        date__range=[start_date,end_date]
    )
    data1 = MouseRecordSerializer(records1, many=True).data
    #print(data1)

    records2 = KeyboardRecord.objects.using('default').filter(username=email).filter(
        date__range=[start_date,end_date]
    )
    data2 = KeyboardRecordSerializer(records2, many=True).data
    #print(data2)

    records3 = AudioRecord.objects.using('default').filter(username=email).filter(
        date__range=[start_date,end_date]
    )
    data3 = AudioRecordSerializer(records3, many=True).data
    #print(data3)

    records4 = VideoRecord.objects.using('default').filter(username=email).filter(
        date__range=[start_date,end_date]
    )
    data4 = VideoRecordSerializer(records4, many=True).data
    #print(data4)

    records5 = WindowRecord.objects.using('default').filter(username=email).filter(
        date__range=[start_date,end_date]
    )
    data5 = WindowRecordSerializer(records5, many=True).data
    #print(data5)

    t1 = datetime.datetime(int(start_date_split[2]), int(start_date_split[1]),
                           int(start_date_split[0]),0,0,1)
    t2 = datetime.datetime(int(end_date_split[2]), int(end_date_split[1]),
                           int(end_date_split[0]),23,59,59)
    log = generate(t1,t2,data1, data2, data3, data4, data5)


    return Response({'message': 'Suspicious behaiviour', 'content':log}, status=status.HTTP_200_OK)


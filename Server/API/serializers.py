from rest_framework import serializers
from API.models import AudioRecord
from API.models import KeyboardRecord
from API.models import MouseRecord
from API.models import WindowRecord
from API.models import VideoRecord

class AudioRecordSerializer(serializers.ModelSerializer):
    class Meta:
        model = AudioRecord
        fields = ('username',
                  'date',
                  'time',
                  'state')
        def create(self, validated_data):
            instance = AudioRecord.objects.db_manager('default').create(**validated_data)
            return instance

    def update(self, instance, validated_data):
        try:
            instance.username = validated_data.get('username', instance.username)
            instance.date = validated_data.get('date', instance.date)
            instance.time = validated_data.get('time', instance.time)
            instance.state = validated_data.get('state', instance.state)
            instance.save(using='default')
        except Exception:
            instance.save(using='default')
            return instance
        return instance


class KeyboardRecordSerializer(serializers.ModelSerializer):
    class Meta:
        model = KeyboardRecord
        fields = ('username',
                  'date',
                  'time',
                  'state')
        def create(self, validated_data):
            instance = KeyboardRecord.objects.db_manager('default').create(**validated_data)
            return instance

    def update(self, instance, validated_data):
        try:
            instance.username = validated_data.get('username', instance.username)
            instance.date = validated_data.get('date', instance.date)
            instance.time = validated_data.get('time', instance.time)
            instance.state = validated_data.get('state', instance.state)
            instance.save(using='default')
        except Exception:
            instance.save(using='default')
            return instance
        return instance


class MouseRecordSerializer(serializers.ModelSerializer):
    class Meta:
        model = MouseRecord
        fields = ('username',
                  'date',
                  'time',
                  'state')
        def create(self, validated_data):
            instance = MouseRecord.objects.db_manager('default').create(**validated_data)
            return instance

    def update(self, instance, validated_data):
        try:
            instance.username = validated_data.get('username', instance.username)
            instance.date = validated_data.get('date', instance.date)
            instance.time = validated_data.get('time', instance.time)
            instance.state = validated_data.get('state', instance.state)
            instance.save(using='default')
        except Exception:
            instance.save(using='default')
            return instance
        return instance


class WindowRecordSerializer(serializers.ModelSerializer):
    class Meta:
        model = WindowRecord
        fields = ('username',
                  'date',
                  'time',
                  'state')
        def create(self, validated_data):
            instance = WindowRecord.objects.db_manager('default').create(**validated_data)
            return instance

    def update(self, instance, validated_data):
        try:
            instance.username = validated_data.get('username', instance.username)
            instance.date = validated_data.get('date', instance.date)
            instance.time = validated_data.get('time', instance.time)
            instance.state = validated_data.get('state', instance.state)
            instance.save(using='default')
        except Exception:
            instance.save(using='default')
            return instance
        return instance


class VideoRecordSerializer(serializers.ModelSerializer):
    class Meta:
        model = VideoRecord
        fields = ('username',
                  'date',
                  'time',
                  'state')
        def create(self, validated_data):
            instance = VideoRecord.objects.db_manager('default').create(**validated_data)
            return instance

    def update(self, instance, validated_data):
        try:
            instance.username = validated_data.get('username', instance.username)
            instance.date = validated_data.get('date', instance.date)
            instance.time = validated_data.get('time', instance.time)
            instance.state = validated_data.get('state', instance.state)
            instance.save(using='default')
        except Exception:
            instance.save(using='default')
            return instance
        return instance
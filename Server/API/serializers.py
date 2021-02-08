from rest_framework import serializers
from API.models import AudioRecord
from API.models import KeyboardRecord
from API.models import MouseRecord
from API.models import WindowRecord

class AudioRecordSerializer(serializers.ModelSerializer):
    class Meta:
        model = AudioRecord
        fields = ('user_id',
                  'date',
                  'time',
                  'state')
        def create(self, validated_data):
            instance = AudioRecord.objects.db_manager('default').create(**validated_data)
            return instance

    def update(self, instance, validated_data):
        try:
            instance.user_id = validated_data.get('user_id', instance.user_id)
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
        fields = ('user_id',
                  'date',
                  'time',
                  'state')
        def create(self, validated_data):
            instance = KeyboardRecord.objects.db_manager('default').create(**validated_data)
            return instance

    def update(self, instance, validated_data):
        try:
            instance.user_id = validated_data.get('user_id', instance.user_id)
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
        fields = ('user_id',
                  'date',
                  'time',
                  'state')
        def create(self, validated_data):
            instance = MouseRecord.objects.db_manager('default').create(**validated_data)
            return instance

    def update(self, instance, validated_data):
        try:
            instance.user_id = validated_data.get('user_id', instance.user_id)
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
        fields = ('user_id',
                  'date',
                  'time',
                  'state')
        def create(self, validated_data):
            instance = WindowRecord.objects.db_manager('default').create(**validated_data)
            return instance

    def update(self, instance, validated_data):
        try:
            instance.user_id = validated_data.get('user_id', instance.user_id)
            instance.date = validated_data.get('date', instance.date)
            instance.time = validated_data.get('time', instance.time)
            instance.state = validated_data.get('state', instance.state)
            instance.save(using='default')
        except Exception:
            instance.save(using='default')
            return instance
        return instance
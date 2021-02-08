from rest_framework import serializers
from API.models import AudioRecord

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
from django.db import models

class VideoFile(models.Model):
    file = models.FileField(upload_to="VideoServer/VideoFiles/")

    @classmethod
    def create(cls, file):
        fileObj = cls(file=file)
        return fileObj

class AudioFile(models.Model):
    file = models.FileField(upload_to="VideoServer/AudioFiles/")

    @classmethod
    def create(cls, file):
        fileObj = cls(file=file)
        return fileObj

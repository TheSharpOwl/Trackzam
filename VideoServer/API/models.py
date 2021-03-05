from django.db import models


import os
from uuid import uuid4
def path_and_rename(instance, filename):
    ext = filename.split('.')[-1]
    # get filename
    if instance.pk:
        filename = '{}.{}'.format(instance.pk, ext)
    else:
        # set filename as random string
        filename = '{}.{}'.format(uuid4().hex, ext)
    # return the whole path to the file
    return os.path.join('VideoServer/VideoFiles/', filename)




class VideoFile(models.Model):
    file = models.FileField(upload_to=path_and_rename)

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

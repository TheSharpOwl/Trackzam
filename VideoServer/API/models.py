from django.db import models
from time import gmtime, strftime

import os
from uuid import uuid4
def path_and_rename_video(instance, filename):
    ext = filename.split('.')[-1]
    # set filename as random string
    time = strftime("%Y-%m-%d-%H-%M-%S", gmtime())
    filename = '{}.{}'.format(time, ext)
    # return the whole path to the file
    return os.path.join('VideoServer/VideoFiles/', filename)

def path_and_rename_audio(instance, filename):
    ext = filename.split('.')[-1]
    # set filename as random string
    time = strftime("%Y-%m-%d-%H-%M-%S", gmtime())
    filename = '{}.{}'.format(time, ext)
    # return the whole path to the file
    return os.path.join('VideoServer/AudioFiles/', filename)



class VideoFile(models.Model):
    file = models.FileField(upload_to=path_and_rename_video)

    @classmethod
    def create(cls, file):
        fileObj = cls(file=file)
        return fileObj

class AudioFile(models.Model):
    file = models.FileField(upload_to=path_and_rename_audio)

    @classmethod
    def create(cls, file):
        fileObj = cls(file=file)
        return fileObj

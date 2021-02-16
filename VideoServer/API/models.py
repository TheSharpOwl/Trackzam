from django.db import models


class File(models.Model):
    file = models.FileField(upload_to="files/")

    @classmethod
    def create(cls, file):
        fileObj = cls(file=file)
        return fileObj
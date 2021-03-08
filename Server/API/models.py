from django.db import models

class AudioRecord(models.Model):
    username = models.CharField(max_length=100, blank=False, default='')
    date = models.DateField(blank=False, auto_now=False)
    time = models.TimeField(blank=False, auto_now=False)
    state = models.CharField(max_length=100, blank=False, default='')

    @classmethod
    def create(cls, username, date, time, state):
        record = cls(username=username, date=date, time=time, state=state)
        return record


class KeyboardRecord(models.Model):
    username = models.CharField(max_length=100, blank=False, default='')
    date = models.DateField(blank=False, auto_now=False)
    time = models.TimeField(blank=False, auto_now=False)
    state = models.CharField(max_length=100, blank=False, default='')

    @classmethod
    def create(cls, username, date, time, state):
        record = cls(username=username, date=date, time=time, state=state)
        return record


class WindowRecord(models.Model):
    username = models.CharField(max_length=100, blank=False, default='')
    date = models.DateField(blank=False, auto_now=False)
    time = models.TimeField(blank=False, auto_now=False)
    state = models.CharField(max_length=100, blank=False, default='')

    @classmethod
    def create(cls, username, date, time, state):
        record = cls(username=username, date=date, time=time, state=state)
        return record


class MouseRecord(models.Model):
    username = models.CharField(max_length=100, blank=False, default='')
    date = models.DateField(blank=False, auto_now=False)
    time = models.TimeField(blank=False, auto_now=False)
    state = models.CharField(max_length=100, blank=False, default='')

    @classmethod
    def create(cls, username, date, time, state):
        record = cls(username=username, date=date, time=time, state=state)
        return record


class VideoRecord(models.Model):
    username = models.CharField(max_length=100, blank=False, default='')
    date = models.DateField(blank=False, auto_now=False)
    time = models.TimeField(blank=False, auto_now=False)
    state = models.CharField(max_length=100, blank=False, default='')

    @classmethod
    def create(cls, username, date, time, state):
        record = cls(username=username, date=date, time=time, state=state)
        return record
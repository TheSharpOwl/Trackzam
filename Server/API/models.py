from django.db import models

class AudioRecord(models.Model):
    user_id = models.IntegerField(blank=False, default=0)
    date = models.DateField(blank=False, auto_now=False)
    time = models.TimeField(blank=False, auto_now=False)
    state = models.CharField(max_length=100, blank=False, default='')

    @classmethod
    def create(cls, user_id, date, time, state):
        record = cls(user_id=user_id, date=date, time=time, state=state)
        return record


class KeyboardRecord(models.Model):
    user_id = models.IntegerField(blank=False, default=0)
    date = models.DateField(blank=False, auto_now=False)
    time = models.TimeField(blank=False, auto_now=False)
    state = models.CharField(max_length=100, blank=False, default='')

    @classmethod
    def create(cls, user_id, date, time, state):
        record = cls(user_id=user_id, date=date, time=time, state=state)
        return record


class WindowRecord(models.Model):
    user_id = models.IntegerField(blank=False, default=0)
    date = models.DateField(blank=False, auto_now=False)
    time = models.TimeField(blank=False, auto_now=False)
    state = models.CharField(max_length=100, blank=False, default='')

    @classmethod
    def create(cls, user_id, date, time, state):
        record = cls(user_id=user_id, date=date, time=time, state=state)
        return record


class MouseRecord(models.Model):
    user_id = models.IntegerField(blank=False, default=0)
    date = models.DateField(blank=False, auto_now=False)
    time = models.TimeField(blank=False, auto_now=False)
    state = models.CharField(max_length=100, blank=False, default='')

    @classmethod
    def create(cls, user_id, date, time, state):
        record = cls(user_id=user_id, date=date, time=time, state=state)
        return record
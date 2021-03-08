from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^api/send_video_file$', views.send_video_file),
    url(r'^api/send_audio_file$', views.send_audio_file),
    url(r'^api/test$', views.test),
]
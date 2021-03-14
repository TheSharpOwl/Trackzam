from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^api/send_audio_logs$', views.send_audio_logs),
    url(r'^api/send_keyboard_logs$', views.send_keyboard_logs),
    url(r'^api/send_mouse_logs$', views.send_mouse_logs),
    url(r'^api/send_window_logs$', views.send_window_logs),
    url(r'^api/send_video_logs$', views.send_video_logs),
    url(r'^api/show_audio_db$', views.show_audio),
    url(r'^api/show_keyboard_db$', views.show_keyboard),
    url(r'^api/show_mouse_db$', views.show_mouse),
    url(r'^api/show_window_db$', views.show_window),
    url(r'^api/show_video_db$', views.show_video),
    url(r'^api/check_user$', views.check_user),
]
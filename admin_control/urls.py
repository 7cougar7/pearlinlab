from django.conf import settings
from django.conf.urls.static import static
from django.urls import path
from admin_control import views

app_name = 'educate'

urlpatterns = [
    path('', views.home_page, name='home_page'),
] + static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)
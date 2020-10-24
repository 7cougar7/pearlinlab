from django.conf import settings
from django.conf.urls.static import static
from django.urls import path
from lab import views

app_name = 'lab'

urlpatterns = [
    path('', views.home_page, name='homePage'),
    path(r'admindash/', views.admin_dashboard, name='adminDash'),
    path(r'joinsession/', views.player_join, name='playerJoin'),
] + static(settings.MEDIA_URL, document_root=settings.MEDIA_ROOT)

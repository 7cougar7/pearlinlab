from django.shortcuts import render


def home_page(request):
    context = {
        'title': 'Home Page'
    }
    return render(request, 'lab/home_page.html', context)


def admin_dashboard(request):
    context = {
        'title': 'Admin Dashboard'
    }
    return render(request, 'lab/landing_page.html', context)


def player_join(request):
    context = {
        'title': 'Join Session'
    }
    return render(request, 'lab/landing_page.html', context)

<?php

namespace App\Http\Controllers\SuperAdmin;

use App\Http\Controllers\Controller;
use App\Models\Quiz;
use App\Models\User;
use Illuminate\View\View;

class DashboardController extends Controller
{
    public function __invoke(): View
    {
        return view('super.dashboard', [
            'usersCount' => User::count(),
            'quizzesCount' => Quiz::count(),
        ]);
    }
}


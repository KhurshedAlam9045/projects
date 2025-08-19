<?php

use App\Http\Controllers\ProfileController;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\Admin\QuizController as AdminQuizController;
use App\Http\Controllers\User\QuizTakeController;
use App\Http\Controllers\SuperAdmin\DashboardController as SuperAdminDashboardController;

Route::get('/', function () {
    return view('welcome');
});

Route::get('/dashboard', function () {
    return view('dashboard');
})->middleware(['auth'])->name('dashboard');

Route::middleware('auth')->group(function () {
    Route::get('/profile', [ProfileController::class, 'edit'])->name('profile.edit');
    Route::patch('/profile', [ProfileController::class, 'update'])->name('profile.update');
    Route::delete('/profile', [ProfileController::class, 'destroy'])->name('profile.destroy');
    Route::get('/purchase', fn () => view('purchase'))->name('purchase');

    // User Dashboard Extras: take quiz
    Route::middleware('role:user')->group(function () {
        Route::get('/quiz', function () {
            $quiz = \App\Models\Quiz::where('is_published', true)->latest()->first();
            if (!$quiz) return redirect()->route('dashboard')->with('status', 'No quiz available.');
            return redirect()->route('quiz.show', $quiz);
        });
        Route::get('/quiz/{quiz}', [QuizTakeController::class, 'show'])->name('quiz.show');
        Route::post('/quiz/{quiz}', [QuizTakeController::class, 'submit'])->name('quiz.submit');
    });

    // Admin: manage quizzes
    Route::prefix('admin')->name('admin.')->middleware('role:admin')->group(function () {
        Route::get('/quizzes', [AdminQuizController::class, 'index'])->name('quizzes.index');
        Route::get('/quizzes/create', [AdminQuizController::class, 'create'])->name('quizzes.create');
        Route::post('/quizzes', [AdminQuizController::class, 'store'])->name('quizzes.store');
    });

    // Super Admin dashboard
    Route::get('/super', SuperAdminDashboardController::class)->middleware('role:super_admin')->name('super.dashboard');
});

require __DIR__.'/auth.php';

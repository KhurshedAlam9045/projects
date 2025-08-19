<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use App\Models\Quiz;
use App\Models\QuizQuestion;
use Illuminate\Http\RedirectResponse;
use Illuminate\Http\Request;
use Illuminate\View\View;

class QuizController extends Controller
{
    public function index(): View
    {
        $quizzes = Quiz::latest()->paginate(10);
        return view('admin.quizzes.index', compact('quizzes'));
    }

    public function create(): View
    {
        return view('admin.quizzes.create');
    }

    public function store(Request $request): RedirectResponse
    {
        $validated = $request->validate([
            'title' => ['required', 'string', 'max:255'],
            'is_published' => ['nullable', 'boolean'],
            'questions' => ['required', 'array', 'size:10'],
            'questions.*.question' => ['required', 'string'],
            'questions.*.option_a' => ['required', 'string'],
            'questions.*.option_b' => ['required', 'string'],
            'questions.*.option_c' => ['required', 'string'],
            'questions.*.option_d' => ['required', 'string'],
            'questions.*.correct_option' => ['required', 'in:a,b,c,d'],
        ]);

        $quiz = Quiz::create([
            'title' => $validated['title'],
            'is_published' => (bool)($validated['is_published'] ?? false),
        ]);

        foreach ($validated['questions'] as $questionData) {
            $quiz->questions()->create($questionData);
        }

        return redirect()->route('admin.quizzes.index')->with('status', 'Quiz created');
    }
}


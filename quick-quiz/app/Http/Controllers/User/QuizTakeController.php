<?php

namespace App\Http\Controllers\User;

use App\Http\Controllers\Controller;
use App\Models\Quiz;
use Illuminate\Http\RedirectResponse;
use Illuminate\Http\Request;
use Illuminate\View\View;

class QuizTakeController extends Controller
{
    public function show(Quiz $quiz): View
    {
        abort_unless($quiz->is_published, 404);
        $questions = $quiz->questions()->inRandomOrder()->take(10)->get();
        return view('user.quiz', compact('quiz', 'questions'));
    }

    public function submit(Request $request, Quiz $quiz): RedirectResponse
    {
        abort_unless($quiz->is_published, 404);
        $validated = $request->validate([
            'answers' => ['required', 'array'],
        ]);

        $questions = $quiz->questions()->orderBy('id')->take(10)->get();
        $score = 0;
        foreach ($questions as $question) {
            $answer = $validated['answers'][$question->id] ?? null;
            if ($answer && $answer === $question->correct_option) {
                $score++;
            }
        }

        if ($score === 10) {
            return redirect()->route('purchase')->with('status', 'Perfect score! Proceed to purchase.');
        }

        return redirect()->route('dashboard')->with('status', "Your score: {$score}/10");
    }
}


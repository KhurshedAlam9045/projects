<?php

namespace Database\Seeders;

use App\Models\User;
use App\Models\Quiz;
use App\Models\QuizQuestion;
// use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\Hash;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     */
    public function run(): void
    {
        // Default user
        User::factory()->create([
            'name' => 'Test User',
            'email' => 'test@example.com',
            'role' => 'user',
        ]);

        // Admin
        User::factory()->create([
            'name' => 'Admin User',
            'email' => 'admin@example.com',
            'password' => Hash::make('password'),
            'role' => 'admin',
        ]);

        // Super Admin
        User::factory()->create([
            'name' => 'Super Admin',
            'email' => 'super@example.com',
            'password' => Hash::make('password'),
            'role' => 'super_admin',
        ]);

        // Sample quiz with 10 questions
        $quiz = Quiz::create([
            'title' => 'Sample Quiz',
            'is_published' => true,
        ]);

        for ($i = 1; $i <= 10; $i++) {
            QuizQuestion::create([
                'quiz_id' => $quiz->id,
                'question' => "Question {$i}?",
                'option_a' => 'Option A',
                'option_b' => 'Option B',
                'option_c' => 'Option C',
                'option_d' => 'Option D',
                'correct_option' => 'a',
            ]);
        }
    }
}
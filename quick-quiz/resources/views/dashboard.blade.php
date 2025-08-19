<x-app-layout>
    <x-slot name="header">
        <h2 class="font-semibold text-xl text-gray-800 dark:text-gray-200 leading-tight">
            {{ __('Dashboard') }}
        </h2>
    </x-slot>

    <div class="py-12">
        <div class="max-w-7xl mx-auto sm:px-6 lg:px-8">
            <div class="bg-white dark:bg-gray-800 overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-6 text-gray-900 dark:text-gray-100">
                    {{ __("You're logged in!") }}
                    <div class="mt-4 space-x-4">
                        @php($role = auth()->user()->role ?? 'user')
                        @if(in_array($role, ['admin','super_admin']))
                            <a href="{{ route('admin.quizzes.index') }}" class="px-3 py-2 bg-blue-600 text-white rounded">Admin Quizzes</a>
                        @endif
                        @if($role === 'super_admin')
                            <a href="{{ route('super.dashboard') }}" class="px-3 py-2 bg-purple-600 text-white rounded">Super Dashboard</a>
                        @endif
                        <a href="{{ url('/quiz') }}" class="px-3 py-2 bg-green-600 text-white rounded">Take Quiz</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</x-app-layout>

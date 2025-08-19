<x-app-layout>
    <x-slot name="header">
        <h2 class="font-semibold text-xl text-gray-800 dark:text-gray-200 leading-tight">
            Quiz: {{ $quiz->title }}
        </h2>
    </x-slot>

    <div class="py-12">
        <div class="max-w-4xl mx-auto sm:px-6 lg:px-8">
            <div class="bg-white dark:bg-gray-800 overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-6 text-gray-900 dark:text-gray-100">
                    <form method="POST" action="{{ route('quiz.submit', $quiz) }}">
                        @csrf
                        @foreach ($questions as $q)
                            <div class="mb-6">
                                <p class="font-semibold">{{ $q->question }}</p>
                                <div class="mt-2 space-y-2">
                                    @foreach (['a' => $q->option_a, 'b' => $q->option_b, 'c' => $q->option_c, 'd' => $q->option_d] as $key => $label)
                                        <label class="block">
                                            <input type="radio" name="answers[{{ $q->id }}]" value="{{ $key }}" class="mr-2">
                                            <span>{{ strtoupper($key) }}. {{ $label }}</span>
                                        </label>
                                    @endforeach
                                </div>
                            </div>
                        @endforeach
                        <button class="px-4 py-2 bg-blue-600 text-white rounded">Submit</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</x-app-layout>


<x-app-layout>
    <x-slot name="header">
        <h2 class="font-semibold text-xl text-gray-800 dark:text-gray-200 leading-tight">
            Admin: Quizzes
        </h2>
    </x-slot>

    <div class="py-12">
        <div class="max-w-7xl mx-auto sm:px-6 lg:px-8">
            <a href="{{ route('admin.quizzes.create') }}" class="inline-block mb-4 px-4 py-2 bg-blue-600 text-white rounded">Create Quiz</a>
            <div class="bg-white dark:bg-gray-800 overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-6 text-gray-900 dark:text-gray-100">
                    <table class="w-full text-left">
                        <thead>
                            <tr>
                                <th class="p-2">Title</th>
                                <th class="p-2">Published</th>
                                <th class="p-2">Questions</th>
                            </tr>
                        </thead>
                        <tbody>
                            @foreach ($quizzes as $quiz)
                                <tr>
                                    <td class="p-2">{{ $quiz->title }}</td>
                                    <td class="p-2">{{ $quiz->is_published ? 'Yes' : 'No' }}</td>
                                    <td class="p-2">{{ $quiz->questions()->count() }}</td>
                                </tr>
                            @endforeach
                        </tbody>
                    </table>
                    <div class="mt-4">{{ $quizzes->links() }}</div>
                </div>
            </div>
        </div>
    </div>
</x-app-layout>


<x-app-layout>
    <x-slot name="header">
        <h2 class="font-semibold text-xl text-gray-800 dark:text-gray-200 leading-tight">
            Admin: Create Quiz (10 Questions)
        </h2>
    </x-slot>

    <div class="py-12">
        <div class="max-w-3xl mx-auto sm:px-6 lg:px-8">
            <div class="bg-white dark:bg-gray-800 overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-6 text-gray-900 dark:text-gray-100">
                    <form method="POST" action="{{ route('admin.quizzes.store') }}">
                        @csrf
                        <div class="mb-4">
                            <label class="block mb-1">Title</label>
                            <input name="title" class="w-full border rounded p-2 text-black" required />
                        </div>
                        <div class="mb-4">
                            <label class="inline-flex items-center">
                                <input type="checkbox" name="is_published" value="1" class="mr-2">
                                <span>Publish</span>
                            </label>
                        </div>
                        @for ($i = 0; $i < 10; $i++)
                            <div class="border-t pt-4 mt-4">
                                <h3 class="font-semibold">Question {{ $i + 1 }}</h3>
                                <div class="mb-2">
                                    <label class="block mb-1">Question</label>
                                    <input name="questions[{{ $i }}][question]" class="w-full border rounded p-2 text-black" required />
                                </div>
                                <div class="grid grid-cols-2 gap-4">
                                    <div>
                                        <label class="block mb-1">Option A</label>
                                        <input name="questions[{{ $i }}][option_a]" class="w-full border rounded p-2 text-black" required />
                                    </div>
                                    <div>
                                        <label class="block mb-1">Option B</label>
                                        <input name="questions[{{ $i }}][option_b]" class="w-full border rounded p-2 text-black" required />
                                    </div>
                                    <div>
                                        <label class="block mb-1">Option C</label>
                                        <input name="questions[{{ $i }}][option_c]" class="w-full border rounded p-2 text-black" required />
                                    </div>
                                    <div>
                                        <label class="block mb-1">Option D</label>
                                        <input name="questions[{{ $i }}][option_d]" class="w-full border rounded p-2 text-black" required />
                                    </div>
                                </div>
                                <div class="mt-2">
                                    <label class="block mb-1">Correct Option</label>
                                    <select name="questions[{{ $i }}][correct_option]" class="border rounded p-2 text-black" required>
                                        <option value="a">A</option>
                                        <option value="b">B</option>
                                        <option value="c">C</option>
                                        <option value="d">D</option>
                                    </select>
                                </div>
                            </div>
                        @endfor
                        <div class="mt-6">
                            <button class="px-4 py-2 bg-green-600 text-white rounded">Save</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</x-app-layout>


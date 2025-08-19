<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Http\Request;
use Symfony\Component\HttpFoundation\Response;

class RoleMiddleware
{
    public function handle(Request $request, Closure $next, string $role): Response
    {
        $user = $request->user();
        if (!$user) {
            abort(403);
        }

        $allowed = match ($role) {
            'user' => in_array($user->role, ['user', 'admin', 'super_admin']),
            'admin' => in_array($user->role, ['admin', 'super_admin']),
            'super_admin' => $user->role === 'super_admin',
            default => false,
        };

        abort_unless($allowed, 403);
        return $next($request);
    }
}


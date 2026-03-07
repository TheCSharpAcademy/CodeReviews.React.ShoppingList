import react from '@vitejs/plugin-react';
import { defineConfig } from 'vite';

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/api': {
        // .NET dev profile serves HTTPS on 7057 by default.
        // Override with VITE_API_PROXY_TARGET if you use a different backend URL.
        target: process.env.VITE_API_PROXY_TARGET ?? 'https://localhost:7057',
        changeOrigin: true,
        secure: false,
      },
    },
  },
});

/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      fontFamily: {
        roboto: ["Roboto", "sans-serif"],
        playwrite: ["Playwrite AU VIC", "cursive"],
      },
      backgroundImage: {
        desk: "url('/desk.jpeg')",
      },
    },
  },
  plugins: [],
};

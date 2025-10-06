const mediaQuery = window.matchMedia('(prefers-color-scheme: dark)');

const updateTheme = () => {
    document.documentElement.setAttribute('data-bs-theme', mediaQuery.matches ? 'dark' : 'light');
}

updateTheme();

mediaQuery.addEventListener('change', updateTheme);

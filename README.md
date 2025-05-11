# ProService Finteco
Aplikacja została zaprojektowana zgodnie z zasadami czystej architektury, co pozwoliło uzyskać przejrzystą strukturę projektu oraz wyraźny podział odpowiedzialności pomiędzy warstwami. Logika domenowa, zawierająca modele zadań, użytkowników oraz wyjątki, znajduje się w osobnej warstwie Core. Na tej podstawie warstwa Application realizuje logikę biznesową, w tym walidację przypisań zadań do użytkowników. Zdefiniowano tam interfejsy usług, obiekty DTO oraz implementacje serwisów operujących na danych.

Komunikacja z użytkownikiem odbywa się przez REST API, zrealizowane w warstwie API, która ogranicza się jedynie do kontrolerów i konfiguracji. Aplikacja Angular pobiera dane i przesyła żądania przydziału zadań, uwzględniając ograniczenia biznesowe.

Po stronie front-endu wdrożono paginację, sortowanie zadań według trudności oraz walidację liczby i typu przypisanych zadań. Formularz przypisywania zawiera walidację. Błędy walidacji biznesowej obsługiwane są przez backend i przekazywane jako kody błędów, które po stronie Angulara tłumaczone są na czytelne komunikaty dla użytkownika.

Całość została zaprojektowana z myślą o bezpieczeństwie danych, czytelności kodu, prostocie rozszerzania funkcjonalności oraz spełnieniu wszystkich wymagań scenariusza projektowego.
## Uruchamianie projektu

Projekt składa się z dwóch niezależnych aplikacji:

### Backend – .NET 8

Aplikacja backendowa znajduje się w katalogu `Finteco.API`.

Aby ją uruchomić:

1. Otwórz plik `Finteco.API.sln` w Visual Studio.
2. Upewnij się, że projekt `Finteco.API` jest ustawiony jako startowy.

Dane są mockowane w pamięci – nie jest wymagana konfiguracja bazy danych.

### Frontend – Angular

Aplikacja frontendowa znajduje się w katalogu `Finteco.Frontend`.
#### Wymagania:

- Node.js 16+ (zalecana wersja LTS)
- Angular CLI

Aby ją uruchomić:

1. Wejdź do katalogu `Finteco.Frontend`:
2. Odpal terminal
3. Zainstaluj zależności 'npm install'
4. Odpal projekt 'ng serve'

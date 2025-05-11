# ProService Finteco
Aplikacja została zaprojektowana zgodnie z zasadami czystej architektury, co pozwoliło uzyskać przejrzystą strukturę projektu oraz wyraźny podział odpowiedzialności pomiędzy warstwami. Logika domenowa, zawierająca modele zadań, użytkowników oraz wyjątki, znajduje się w osobnej warstwie Core. Na tej podstawie warstwa Application realizuje logikę biznesową, w tym walidację przypisań zadań do użytkowników. Zdefiniowano tam interfejsy usług, obiekty DTO oraz implementacje serwisów operujących na danych.

Komunikacja z użytkownikiem odbywa się przez REST API, zrealizowane w warstwie API, która ogranicza się jedynie do kontrolerów i konfiguracji. Aplikacja Angular pobiera dane i przesyła żądania przydziału zadań, uwzględniając ograniczenia biznesowe.

Po stronie front-endu wdrożono paginację, sortowanie zadań według trudności oraz walidację liczby i typu przypisanych zadań. Formularz przypisywania zawiera walidację. Błędy walidacji biznesowej obsługiwane są przez backend i przekazywane jako kody błędów, które po stronie Angulara tłumaczone są na czytelne komunikaty dla użytkownika.

Całość została zaprojektowana z myślą o bezpieczeństwie danych, czytelności kodu, prostocie rozszerzania funkcjonalności oraz spełnieniu wszystkich wymagań scenariusza projektowego.

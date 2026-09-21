namespace MinonTweaks;

// Полный каталог твиков для 5 вкладок — RU-текст из HTML-дизайна дословно, EN — перевод.
public static class TweakCatalog
{
    public static Dictionary<string, List<TweakGroup>> Pages { get; } = new()
    {
        ["system"] = new List<TweakGroup>
        {
            new TweakGroup { LabelRu = "Базовые твики", LabelEn = "Base Tweaks", Items = new List<TweakItem>
            {
                new() { TitleRu = "Приоритеты планировщика", TitleEn = "Scheduler Priorities", DescriptionRu = "Оптимизирует распределение квантов времени процессора.", DescriptionEn = "Optimizes CPU time-slice distribution.", Badge = "safe", IsOn = true },
                new() { TitleRu = "Отключить MPO (GPU Fix)", TitleEn = "Disable MPO (GPU Fix)", DescriptionRu = "Убирает Multi-Plane Overlay — источник микро-фризов.", DescriptionEn = "Removes Multi-Plane Overlay — a source of micro-stutters.", Badge = "safe", IsOn = true },
                new() { TitleRu = "MSI Mode GPU", TitleEn = "MSI Mode GPU", DescriptionRu = "Переводит GPU в режим быстрых прерываний.", DescriptionEn = "Switches the GPU to fast interrupt mode.", Badge = "safe", IsOn = true },
                new() { TitleRu = "Таймеры BCD", TitleEn = "BCD Timers", DescriptionRu = "Отключает HPET и динамические тики для плавного фреймтайма.", DescriptionEn = "Disables HPET and dynamic ticks for smoother frame times.", Badge = "risky", IsOn = false },
            }},
            new TweakGroup { LabelRu = "Питание и производительность", LabelEn = "Power & Performance", Items = new List<TweakItem>
            {
                new() { TitleRu = "High Performance Plan", TitleEn = "High Performance Plan", DescriptionRu = "Переключает план электропитания на Maximum Performance.", DescriptionEn = "Switches the power plan to Maximum Performance.", Badge = "safe", IsOn = true },
                new() { TitleRu = "Отключить Mitigations", TitleEn = "Disable Mitigations", DescriptionRu = "Снимает защиту Spectre/Meltdown. Даёт прирост CPU.", DescriptionEn = "Removes Spectre/Meltdown protections. Boosts CPU performance.", Badge = "risky", IsOn = false },
                new() { TitleRu = "Отключить Windows Update", TitleEn = "Disable Windows Update", DescriptionRu = "Останавливает службу автообновлений во время игры.", DescriptionEn = "Stops the auto-update service while gaming.", Badge = "risky", IsOn = false },
                new() { TitleRu = "Game Mode Scheduler", TitleEn = "Game Mode Scheduler", DescriptionRu = "Включает системный Game Mode для приоритизации игр.", DescriptionEn = "Enables system Game Mode to prioritize games.", Badge = "safe", IsOn = true },
            }},
            new TweakGroup { LabelRu = "Диск и IO", LabelEn = "Disk & IO", Items = new List<TweakItem>
            {
                new() { TitleRu = "Prefetch / SuperFetch", TitleEn = "Prefetch / SuperFetch", DescriptionRu = "Отключает SysMain и предзагрузку (рекомендовано для SSD).", DescriptionEn = "Disables SysMain and preloading (recommended for SSDs).", Badge = "safe", IsOn = true },
                new() { TitleRu = "IO Tweaks + Boost", TitleEn = "IO Tweaks + Boost", DescriptionRu = "Приоритет дисковых операций для игрового процесса.", DescriptionEn = "Prioritizes disk operations for the game process.", Badge = "safe", IsOn = false },
            }},
        },

        ["network"] = new List<TweakGroup>
        {
            new TweakGroup { LabelRu = "TCP / IP", LabelEn = "TCP / IP", Items = new List<TweakItem>
            {
                new() { TitleRu = "TCP Optimizer (Nagle)", TitleEn = "TCP Optimizer (Nagle)", DescriptionRu = "Данные летят на сервер мгновенно без буферизации.", DescriptionEn = "Data reaches the server instantly, without buffering.", Badge = "safe", IsOn = true },
                new() { TitleRu = "Сетевой троттлинг", TitleEn = "Network Throttling", DescriptionRu = "Снимает системный лимит на пропускную способность.", DescriptionEn = "Removes the system bandwidth limit.", Badge = "safe", IsOn = true },
                new() { TitleRu = "Энергосбережение сети", TitleEn = "Network Power Saving", DescriptionRu = "Запрещает сетевой карте засыпать во время игры.", DescriptionEn = "Prevents the network adapter from sleeping during play.", Badge = "safe", IsOn = true },
            }},
            new TweakGroup { LabelRu = "Дополнительно", LabelEn = "Advanced", Items = new List<TweakItem>
            {
                new() { TitleRu = "Netadapter Processor", TitleEn = "Netadapter Processor", DescriptionRu = "Включает RSS и привязку очередей сетевой карты к ядрам CPU.", DescriptionEn = "Enables RSS and pins network queues to CPU cores.", Badge = "safe", IsOn = false },
                new() { TitleRu = "TCP Autotuning Off", TitleEn = "TCP Autotuning Off", DescriptionRu = "Отключает авто-масштабирование TCP буфера для стабильного пинга.", DescriptionEn = "Disables TCP buffer auto-scaling for stable ping.", Badge = "risky", IsOn = false },
                new() { TitleRu = "Сброс DNS + приоритет", TitleEn = "Flush DNS + Priority", DescriptionRu = "Очищает кэш DNS и выставляет приоритет 1.1.1.1 / 8.8.8.8.", DescriptionEn = "Clears the DNS cache and prioritizes 1.1.1.1 / 8.8.8.8.", ActionLabelRu = "Очистить DNS", ActionLabelEn = "Flush DNS" },
                new() { TitleRu = "Сброс Winsock", TitleEn = "Reset Winsock", DescriptionRu = "Хард-ресет сетевых протоколов.", DescriptionEn = "Hard reset of network protocols.", Badge = "risky", ActionLabelRu = "Сброс", ActionLabelEn = "Reset" },
            }},
        },

        ["gaming"] = new List<TweakGroup>
        {
            new TweakGroup { LabelRu = "Fortnite Engine", LabelEn = "Fortnite Engine", Items = new List<TweakItem>
            {
                new() { TitleRu = "Приоритет Fortnite (High)", TitleEn = "Fortnite Priority (High)", DescriptionRu = "Принудительно выдаёт процессу игры максимальный приоритет ЦП.", DescriptionEn = "Forces the game process to run at maximum CPU priority.", Badge = "safe", IsOn = true },
                new() { TitleRu = "Стриминг текстур", TitleEn = "Texture Streaming", DescriptionRu = "Оптимизация движка под быстрый диск.", DescriptionEn = "Engine optimization tuned for fast drives.", Badge = "safe", IsOn = true },
                new() { TitleRu = "DirectX Tweaks", TitleEn = "DirectX Tweaks", DescriptionRu = "Оптимизация рендер-квот для стабильного FPS.", DescriptionEn = "Optimizes render quotas for stable FPS.", Badge = "safe", IsOn = false },
            }},
            new TweakGroup { LabelRu = "Input Tweaks", LabelEn = "Input Tweaks", Items = new List<TweakItem>
            {
                new() { TitleRu = "Mouse Tweaks", TitleEn = "Mouse Tweaks", DescriptionRu = "Отключает Enhanced Precision, включает Raw Input фикс.", DescriptionEn = "Disables Enhanced Precision, enables the Raw Input fix.", Badge = "safe", IsOn = true },
                new() { TitleRu = "Keyboard Latency", TitleEn = "Keyboard Latency", DescriptionRu = "Снижает задержку ввода с клавиатуры до минимума.", DescriptionEn = "Reduces keyboard input latency to a minimum.", Badge = "safe", IsOn = false },
                new() { TitleRu = "Standby RAM", TitleEn = "Standby RAM", DescriptionRu = "Очистка кэша памяти перед матчем.", DescriptionEn = "Clears the memory cache before a match.", ActionLabelRu = "Очистить RAM", ActionLabelEn = "Clear RAM" },
            }},
            new TweakGroup { LabelRu = "Engine Tweaks (Advanced)", LabelEn = "Engine Tweaks (Advanced)", Items = new List<TweakItem>
            {
                new() { TitleRu = "Engine.ini оптимизация", TitleEn = "Engine.ini Optimization", DescriptionRu = "Стриминг текстур, анизотропия, асинхронная загрузка — ниже уровня графических слайдеров.", DescriptionEn = "Texture streaming, anisotropy, async loading — beyond what the graphics sliders offer.", Badge = "risky", ActionLabelsRu = new[] { "Применить", "Откатить" }, ActionLabelsEn = new[] { "Apply", "Revert" } },
                new() { TitleRu = "Исключить ShaderCompileWorker из АВ", TitleEn = "Exclude ShaderCompileWorker from AV", DescriptionRu = "Ускоряет компиляцию шейдеров на 200%+, убирая антивирус из процесса.", DescriptionEn = "Speeds up shader compilation by 200%+ by removing the antivirus from the process.", Badge = "risky", ActionLabelsRu = new[] { "Исключить", "Вернуть" }, ActionLabelsEn = new[] { "Exclude", "Restore" } },
                new() { TitleRu = "WorkerProcessPriority", TitleEn = "WorkerProcessPriority", DescriptionRu = "Приоритет потока компиляции шейдеров = Normal вместо Low.", DescriptionEn = "Sets the shader compile thread priority to Normal instead of Low.", Badge = "safe", IsOn = false },
                new() { TitleRu = "Системная диагностика (SFC/DISM)", TitleEn = "System Diagnostics (SFC/DISM)", DescriptionRu = "Чинит повреждённые системные файлы, которые незаметно душат FPS.", DescriptionEn = "Repairs corrupted system files that silently choke your FPS.", ActionLabelRu = "Запустить проверку", ActionLabelEn = "Run Check" },
            }},
        },

        ["gpu"] = new List<TweakGroup>
        {
            new TweakGroup { LabelRu = "Nvidia", LabelEn = "Nvidia", Items = new List<TweakItem>
            {
                new() { TitleRu = "Disable P-State", TitleEn = "Disable P-State", DescriptionRu = "Фиксирует частоту GPU на максимуме, убирая скачки FPS.", DescriptionEn = "Locks GPU clock at maximum, removing FPS spikes.", Badge = "risky", IsOn = false },
                new() { TitleRu = "Remove Nvidia Telemetry", TitleEn = "Remove Nvidia Telemetry", DescriptionRu = "Удаляет службы слежения NvTelemetry* в фоне.", DescriptionEn = "Removes the background NvTelemetry* tracking services.", Badge = "safe", IsOn = true },
                new() { TitleRu = "Disable Preemptions", TitleEn = "Disable Preemptions", DescriptionRu = "Убирает прерывания GPU для стабильного фреймтайма.", DescriptionEn = "Removes GPU preemptions for stable frame times.", Badge = "risky", IsOn = false },
                new() { TitleRu = "Latency Tolerance", TitleEn = "Latency Tolerance", DescriptionRu = "LatencyToleranceOverride для минимальной задержки GPU.", DescriptionEn = "LatencyToleranceOverride for minimal GPU latency.", Badge = "safe", IsOn = false },
            }},
            new TweakGroup { LabelRu = "AMD / Radeon", LabelEn = "AMD / Radeon", Items = new List<TweakItem>
            {
                new() { TitleRu = "Disable AMD Telemetry", TitleEn = "Disable AMD Telemetry", DescriptionRu = "Отключает сбор данных AMD User Experience Program.", DescriptionEn = "Disables AMD User Experience Program data collection.", Badge = "safe", IsOn = true },
                new() { TitleRu = "AMD Startup Clean", TitleEn = "AMD Startup Clean", DescriptionRu = "Убирает лишние процессы Radeon из автозагрузки.", DescriptionEn = "Removes unnecessary Radeon processes from startup.", Badge = "safe", ActionLabelRu = "Очистить", ActionLabelEn = "Clean" },
            }},
            new TweakGroup { LabelRu = "Общие", LabelEn = "General", Items = new List<TweakItem>
            {
                new() { TitleRu = "Disable HDCP", TitleEn = "Disable HDCP", DescriptionRu = "Отключает проверку HDCP для снижения задержки на дисплее.", DescriptionEn = "Disables HDCP checks to reduce display latency.", Badge = "risky", IsOn = false },
                new() { TitleRu = "Disable Power Gating", TitleEn = "Disable Power Gating", DescriptionRu = "Запрещает GPU снижать напряжение в простое.", DescriptionEn = "Prevents the GPU from lowering voltage when idle.", Badge = "risky", IsOn = false },
            }},
        },

        ["debloat"] = new List<TweakGroup>
        {
            new TweakGroup { LabelRu = "Службы", LabelEn = "Services", Items = new List<TweakItem>
            {
                new() { TitleRu = "Службы Windows Update", TitleEn = "Windows Update Services", DescriptionRu = "Отключает Update + Store фоновые службы.", DescriptionEn = "Disables Update + Store background services.", Badge = "risky", IsOn = false },
                new() { TitleRu = "Bluetooth службы", TitleEn = "Bluetooth Services", DescriptionRu = "Если не используешь Bluetooth — безопасно отключить.", DescriptionEn = "Safe to disable if you don't use Bluetooth.", Badge = "safe", IsOn = false },
                new() { TitleRu = "Print Spooler", TitleEn = "Print Spooler", DescriptionRu = "Служба печати. Не нужна геймерам.", DescriptionEn = "Print service. Not needed for gaming.", Badge = "safe", IsOn = true },
                new() { TitleRu = "Remote Services", TitleEn = "Remote Services", DescriptionRu = "Remote Registry, Remote Desktop — отключить для безопасности.", DescriptionEn = "Remote Registry, Remote Desktop — disable for security.", Badge = "safe", IsOn = true },
                new() { TitleRu = "WiFi Services (Ethernet)", TitleEn = "WiFi Services (Ethernet)", DescriptionRu = "Отключает WiFi службы если используешь Ethernet кабель.", DescriptionEn = "Disables WiFi services if you use an Ethernet cable.", Badge = "safe", IsOn = false },
                new() { TitleRu = "Лишние службы", TitleEn = "Unneeded Services", DescriptionRu = "Fax, диагностика, поиск. Не нужны для игры.", DescriptionEn = "Fax, diagnostics, search. Not needed for gaming.", Badge = "safe", IsOn = true },
            }},
            new TweakGroup { LabelRu = "Автозагрузка и приложения", LabelEn = "Startup & Apps", Items = new List<TweakItem>
            {
                new() { TitleRu = "Автозагрузка", TitleEn = "Startup", DescriptionRu = "Открыть системный менеджер автозагрузки.", DescriptionEn = "Open the system startup manager.", ActionLabelRu = "Открыть", ActionLabelEn = "Open" },
                new() { TitleRu = "Store Apps", TitleEn = "Store Apps", DescriptionRu = "Удаляет предустановленные приложения Microsoft Store.", DescriptionEn = "Removes preinstalled Microsoft Store apps.", Badge = "risky", ActionLabelRu = "Удалить", ActionLabelEn = "Remove" },
                new() { TitleRu = "Debloat Chrome", TitleEn = "Debloat Chrome", DescriptionRu = "Отключает фоновые процессы и телеметрию Chrome.", DescriptionEn = "Disables Chrome's background processes and telemetry.", ActionLabelRu = "Применить", ActionLabelEn = "Apply" },
                new() { TitleRu = "Temp файлы", TitleEn = "Temp Files", DescriptionRu = "Очищает временные файлы системы.", DescriptionEn = "Clears system temporary files.", ActionLabelRu = "Очистить", ActionLabelEn = "Clean" },
            }},
        },
    };
}

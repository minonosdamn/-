using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace MinonTweaks;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private readonly Random _rnd = new();

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        SpawnParticles();
        StartBrandFlicker();
        PopulateTweakPages();
        ApplyActiveNav("home");
    }

    // Заполняет 5 страниц (System/Network/Gaming/GPU/Debloat) карточками из TweakCatalog —
    // тот же принцип, что renderTweaks() в HTML: данные отдельно, разметка строится из них.
    private void PopulateTweakPages()
    {
        SystemContentWrapper.Content = BuildTweakPage(TweakCatalog.Pages["system"]);
        NetworkContentWrapper.Content = BuildTweakPage(TweakCatalog.Pages["network"]);
        GamingContentWrapper.Content = BuildTweakPage(TweakCatalog.Pages["gaming"]);
        GpuContentWrapper.Content = BuildTweakPage(TweakCatalog.Pages["gpu"]);
        DebloatContentWrapper.Content = BuildTweakPage(TweakCatalog.Pages["debloat"]);
    }

    private UIElement BuildTweakPage(List<TweakGroup> groups)
    {
        var root = new StackPanel();
        bool first = true;
        foreach (var group in groups)
        {
            root.Children.Add(BuildSectionLabel(group, first));
            first = false;
            root.Children.Add(BuildTweakGrid(group.Items));
        }
        return root;
    }

    // Заголовок секции ("Базовые твики" и т.п.) — жёлтый, с подсветкой и нижней линией
    private UIElement BuildSectionLabel(TweakGroup group, bool isFirst)
    {
        var panel = new StackPanel { Margin = new Thickness(0, isFirst ? 0 : 26, 0, 12) };
        var text = new TextBlock
        {
            Text = group.Label(_currentLang).ToUpperInvariant(),
            Foreground = (Brush)FindResource("YellowBrush"),
            FontSize = 10.5,
            FontWeight = FontWeights.Bold,
            Effect = new DropShadowEffect
            {
                Color = (Color)FindResource("YellowColor"),
                BlurRadius = 10,
                ShadowDepth = 0,
                Opacity = 0.35
            }
        };
        var line = new Border
        {
            BorderBrush = new SolidColorBrush(Color.FromArgb(0x2E, 0xFF, 0xE3, 0x00)),
            BorderThickness = new Thickness(0, 0, 0, 1),
            Margin = new Thickness(0, 8, 0, 0)
        };
        panel.Children.Add(text);
        panel.Children.Add(line);
        return panel;
    }

    // Сетка карточек 2 в ряд (как .tweak-grid в HTML)
    private UIElement BuildTweakGrid(List<TweakItem> items)
    {
        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition());
        grid.ColumnDefinitions.Add(new ColumnDefinition());

        int rows = (int)Math.Ceiling(items.Count / 2.0);
        for (int r = 0; r < rows; r++)
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        for (int i = 0; i < items.Count; i++)
        {
            int row = i / 2;
            int col = i % 2;
            var card = BuildTweakCard(items[i]);
            card.Margin = new Thickness(col == 0 ? 0 : 6, 0, col == 0 ? 6 : 0, 12);
            Grid.SetRow(card, row);
            Grid.SetColumn(card, col);
            grid.Children.Add(card);
        }
        return grid;
    }

    // Одна карточка твика: заголовок + бейдж, описание, тумблер ИЛИ кнопка(и)-действие
    private Border BuildTweakCard(TweakItem item)
    {
        var lift = new TranslateTransform();
        var card = new Border { Style = (Style)FindResource("TweakCardStyle"), RenderTransform = lift };
        card.MouseEnter += (s, e) => lift.BeginAnimation(TranslateTransform.YProperty,
            new DoubleAnimation(-4, TimeSpan.FromSeconds(0.15)));
        card.MouseLeave += (s, e) => lift.BeginAnimation(TranslateTransform.YProperty,
            new DoubleAnimation(0, TimeSpan.FromSeconds(0.15)));

        var topGrid = new Grid();
        topGrid.ColumnDefinitions.Add(new ColumnDefinition());
        topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        var leftStack = new StackPanel();

        var titleRow = new WrapPanel();
        titleRow.Children.Add(new TextBlock
        {
            Text = item.Title(_currentLang),
            FontSize = 13.5,
            FontWeight = FontWeights.Bold,
            Foreground = Brushes.White,
            Margin = new Thickness(0, 0, 8, 4),
            VerticalAlignment = VerticalAlignment.Center
        });
        if (item.Badge != null)
            titleRow.Children.Add(BuildBadge(item.Badge));
        leftStack.Children.Add(titleRow);

        leftStack.Children.Add(new TextBlock
        {
            Text = item.Description(_currentLang),
            FontSize = 11.5,
            Foreground = (Brush)FindResource("MutedBrush"),
            TextWrapping = TextWrapping.Wrap,
            MaxWidth = 340,
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(0, 2, 0, 0)
        });

        var actionLabels = item.ActionLabels(_currentLang);
        var actionLabel = item.ActionLabel(_currentLang);
        bool isToggle = actionLabel == null && actionLabels == null;

        if (actionLabels != null)
        {
            var actionsPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 12, 0, 0) };
            actionsPanel.Children.Add(new Button
            {
                Content = actionLabels[0],
                Style = (Style)FindResource("TweakPrimaryButtonStyle"),
                Margin = new Thickness(0, 0, 8, 0)
            });
            actionsPanel.Children.Add(new Button
            {
                Content = actionLabels[1],
                Style = (Style)FindResource("TweakGhostButtonStyle")
            });
            leftStack.Children.Add(actionsPanel);
        }
        else if (actionLabel != null)
        {
            var actionsPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 12, 0, 0) };
            actionsPanel.Children.Add(new Button
            {
                Content = actionLabel,
                Style = (Style)FindResource("TweakGhostButtonStyle")
            });
            leftStack.Children.Add(actionsPanel);
        }

        Grid.SetColumn(leftStack, 0);
        topGrid.Children.Add(leftStack);

        if (isToggle)
        {
            var toggle = new ToggleButton
            {
                Style = (Style)FindResource("NeonToggleStyle"),
                IsChecked = item.IsOn,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(12, 2, 0, 0)
            };
            Grid.SetColumn(toggle, 1);
            topGrid.Children.Add(toggle);
        }

        card.Child = topGrid;
        return card;
    }

    // Бейдж Safe/Risky — цветная пилюля со свечением
    private Border BuildBadge(string badgeType)
    {
        bool safe = badgeType == "safe";
        var brush = (Brush)FindResource(safe ? "GreenBrush" : "RedBrush");
        var color = (Color)FindResource(safe ? "GreenColor" : "RedColor");

        var badge = new Border
        {
            CornerRadius = new CornerRadius(8),
            BorderBrush = brush,
            BorderThickness = new Thickness(1),
            Background = new SolidColorBrush(Color.FromArgb(0x14, color.R, color.G, color.B)),
            Padding = new Thickness(8, 2, 8, 2),
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(0, 0, 0, 4)
        };
        badge.Child = new TextBlock
        {
            Text = safe ? "SAFE" : "RISKY",
            FontSize = 9,
            FontWeight = FontWeights.Bold,
            Foreground = brush,
            Effect = new DropShadowEffect { Color = color, BlurRadius = 6, ShadowDepth = 0, Opacity = 0.5 }
        };
        return badge;
    }

    // Плавающие частицы на фоне — аналог particle-canvas из HTML-версии
    private void SpawnParticles()
    {
        const int count = 26;
        for (int i = 0; i < count; i++)
        {
            bool bright = _rnd.NextDouble() < 0.18;
            double size = bright ? _rnd.NextDouble() * 2 + 2.5 : _rnd.NextDouble() * 1.3 + 1;

            var dot = new Ellipse
            {
                Width = size,
                Height = size,
                Fill = (Brush)FindResource("YellowBrush"),
                Opacity = bright ? 0.6 : 0.22
            };
            if (bright)
            {
                dot.Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = (Color)FindResource("YellowColor"),
                    BlurRadius = 8,
                    ShadowDepth = 0,
                    Opacity = 0.9
                };
            }

            double startX = _rnd.NextDouble() * 1260;
            double startY = _rnd.NextDouble() * 780;
            Canvas.SetLeft(dot, startX);
            Canvas.SetTop(dot, startY);
            ParticleCanvas.Children.Add(dot);

            // Медленный случайный дрейф туда-обратно
            double driftX = (_rnd.NextDouble() - 0.5) * 90;
            double driftY = (_rnd.NextDouble() - 0.5) * 90;
            double duration = _rnd.NextDouble() * 8 + 8;

            var animX = new DoubleAnimation(startX, startX + driftX, TimeSpan.FromSeconds(duration))
            {
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };
            var animY = new DoubleAnimation(startY, startY + driftY, TimeSpan.FromSeconds(duration * 1.15))
            {
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };
            dot.BeginAnimation(Canvas.LeftProperty, animX);
            dot.BeginAnimation(Canvas.TopProperty, animY);
        }
    }

    // Лёгкое мерцание бренда в шапке — как flicker-анимация в HTML-версии
    private void StartBrandFlicker()
    {
        var flicker = new DoubleAnimationUsingKeyFrames { RepeatBehavior = RepeatBehavior.Forever };
        flicker.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, KeyTime.FromPercent(0)));
        flicker.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, KeyTime.FromPercent(0.78)));
        flicker.KeyFrames.Add(new LinearDoubleKeyFrame(0.82, KeyTime.FromPercent(0.80)));
        flicker.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, KeyTime.FromPercent(0.82)));
        flicker.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, KeyTime.FromPercent(1.0)));
        Storyboard.SetTarget(flicker, BrandText);
        Storyboard.SetTargetProperty(flicker, new PropertyPath(OpacityProperty));

        var sb = new Storyboard { Duration = TimeSpan.FromSeconds(6) };
        sb.Children.Add(flicker);
        sb.Begin();
    }

    private string _currentLang = "ru";
    private string _currentTab = "home";

    private readonly Dictionary<string, Dictionary<string, string>> _navLabels = new()
    {
        ["ru"] = new()
        {
            { "home", "Главная" }, { "resources", "Ресурсы" }, { "system", "Система" },
            { "network", "Сеть и Пинг" }, { "gaming", "Профиль Игр" }, { "gpu", "GPU Tweaks" }, { "debloat", "Debloat" }
        },
        ["en"] = new()
        {
            { "home", "Home" }, { "resources", "Resources" }, { "system", "System" },
            { "network", "Network & Ping" }, { "gaming", "Game Profile" }, { "gpu", "GPU Tweaks" }, { "debloat", "Debloat" }
        }
    };

    private readonly Dictionary<string, Dictionary<string, string>> _pageTitlesByLang = new()
    {
        ["ru"] = new()
        {
            { "home", "Главная" }, { "resources", "Ресурсы" }, { "system", "Система" },
            { "network", "Сеть и Пинг" }, { "gaming", "Профиль Игр" }, { "gpu", "GPU Tweaks" }, { "debloat", "Debloat" }
        },
        ["en"] = new()
        {
            { "home", "Home" }, { "resources", "Resources" }, { "system", "System" },
            { "network", "Network & Ping" }, { "gaming", "Game Profile" }, { "gpu", "GPU Tweaks" }, { "debloat", "Debloat" }
        }
    };

    private readonly Dictionary<string, Dictionary<string, string>> _pageSubtitlesByLang = new()
    {
        ["ru"] = new()
        {
            { "home", "Мониторинг системы в реальном времени" },
            { "resources", "Мониторинг производительности в реальном времени" },
            { "system", "Системные фиксы для снижения Input Lag" },
            { "network", "Твики протоколов для минимизации пинга" },
            { "gaming", "Приоритеты, движок и RAM для соревновательного Fortnite" },
            { "gpu", "Твики видеокарты для максимальной производительности" },
            { "debloat", "Отключение лишних служб и приложений" },
        },
        ["en"] = new()
        {
            { "home", "Real-time system monitoring" },
            { "resources", "Real-time performance monitoring" },
            { "system", "System fixes to reduce input lag" },
            { "network", "Protocol tweaks to minimize ping" },
            { "gaming", "Priorities, engine and RAM for competitive Fortnite" },
            { "gpu", "GPU tweaks for maximum performance" },
            { "debloat", "Disable unnecessary services and apps" },
        }
    };

    // Порядок ключей нужен только для перебора при подсветке навигации — берём его из _navLabels["ru"]
    private IEnumerable<string> AllPageKeys => _navLabels["ru"].Keys;

    private void NavItem_Click(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Border clicked || clicked.Tag is not string key) return;
        ApplyActiveNav(key);
    }

    private void ApplyActiveNav(string key)
    {
        _currentTab = key;

        // Сбрасываем подсветку всех пунктов, включаем только у выбранного; заодно обновляем текст под язык
        foreach (var pageKey in AllPageKeys)
        {
            var border = FindName($"Nav_{pageKey}") as Border;
            var textBlock = FindName($"NavText_{pageKey}") as TextBlock;
            if (border == null || textBlock == null) continue;

            textBlock.Text = _navLabels[_currentLang][pageKey];
            bool isActive = pageKey == key;

            if (isActive)
            {
                border.BorderBrush = (Brush)FindResource("YellowBrush");
                border.Background = new LinearGradientBrush(
                    Color.FromArgb(0x1F, 0xFF, 0xE3, 0x00),
                    Colors.Transparent, new Point(0, 0.5), new Point(1, 0.5));
                textBlock.Foreground = Brushes.White;
                textBlock.FontWeight = FontWeights.SemiBold;
                textBlock.Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = (Color)FindResource("YellowColor"),
                    BlurRadius = 8,
                    ShadowDepth = 0,
                    Opacity = 0.6
                };
            }
            else
            {
                border.BorderBrush = Brushes.Transparent;
                border.Background = Brushes.Transparent;
                textBlock.Foreground = (Brush)FindResource("MutedBrush");
                textBlock.FontWeight = FontWeights.Normal;
                textBlock.Effect = null;
            }
        }

        // Обновляем заголовок топбара
        PageTitleText.Text = _pageTitlesByLang[_currentLang][key];
        PageSubtitleText.Text = _pageSubtitlesByLang[_currentLang][key];

        // Все 7 разделов теперь с реальным контентом — просто показываем нужный, остальные скрываем
        HomeContentWrapper.Visibility = key == "home" ? Visibility.Visible : Visibility.Collapsed;
        ResourcesContentWrapper.Visibility = key == "resources" ? Visibility.Visible : Visibility.Collapsed;
        SystemContentWrapper.Visibility = key == "system" ? Visibility.Visible : Visibility.Collapsed;
        NetworkContentWrapper.Visibility = key == "network" ? Visibility.Visible : Visibility.Collapsed;
        GamingContentWrapper.Visibility = key == "gaming" ? Visibility.Visible : Visibility.Collapsed;
        GpuContentWrapper.Visibility = key == "gpu" ? Visibility.Visible : Visibility.Collapsed;
        DebloatContentWrapper.Visibility = key == "debloat" ? Visibility.Visible : Visibility.Collapsed;
    }

    private void LangRu_Click(object sender, RoutedEventArgs e) => SetLanguage("ru");
    private void LangEn_Click(object sender, RoutedEventArgs e) => SetLanguage("en");

    private void SetLanguage(string lang)
    {
        if (_currentLang == lang) return;
        _currentLang = lang;
        bool isRu = lang == "ru";

        // Подсветка активной кнопки языка
        var glow = new DropShadowEffect { Color = (Color)FindResource("YellowColor"), BlurRadius = 20, ShadowDepth = 0, Opacity = 0.5 };
        Btn_LangRu.Background = isRu ? (Brush)FindResource("YellowBrush") : (Brush)FindResource("Panel2Brush");
        Btn_LangRu.Foreground = isRu ? new SolidColorBrush(Color.FromRgb(0x11, 0x11, 0x11)) : (Brush)FindResource("MutedBrush");
        Btn_LangRu.Effect = isRu ? glow : null;
        Btn_LangEn.Background = !isRu ? (Brush)FindResource("YellowBrush") : (Brush)FindResource("Panel2Brush");
        Btn_LangEn.Foreground = !isRu ? new SolidColorBrush(Color.FromRgb(0x11, 0x11, 0x11)) : (Brush)FindResource("MutedBrush");
        Btn_LangEn.Effect = !isRu ? new DropShadowEffect { Color = (Color)FindResource("YellowColor"), BlurRadius = 20, ShadowDepth = 0, Opacity = 0.5 } : null;

        // ===== Главная =====
        Lbl_Cpu.Text = isRu ? "Процессор: " : "CPU: ";
        Lbl_Gpu.Text = isRu ? "Видеокарта: " : "GPU: ";
        Lbl_GameDrive.Text = isRu ? "Диск игры: " : "Game Drive: ";
        Lbl_CpuUsage.Text = isRu ? "ЗАГРУЗКА CPU" : "CPU USAGE";
        Lbl_GpuUsage.Text = isRu ? "ЗАГРУЗКА GPU" : "GPU USAGE";
        Lbl_RamUsage.Text = isRu ? "ЗАГРУЗКА RAM" : "RAM USAGE";
        Lbl_Temperature.Text = isRu ? "ТЕМПЕРАТУРА" : "TEMPERATURE";
        Lbl_Disk.Text = isRu ? "ДИСК" : "DISK";
        Lbl_DiskPath.Text = isRu ? @"D:\Epic Games\Fortnite — 214 ГБ занято из 512 ГБ" : @"D:\Epic Games\Fortnite — 214 GB used of 512 GB";
        Lbl_QuickStats.Text = isRu ? "БЫСТРАЯ СТАТИСТИКА" : "QUICK STATS";
        Lbl_TempCpu.Text = isRu ? "Темп. CPU" : "CPU Temp";
        Lbl_TempGpu.Text = isRu ? "Темп. GPU" : "GPU Temp";
        Lbl_RamUsed.Text = isRu ? "RAM занято" : "RAM Used";
        Lbl_Safety.Text = isRu ? "БЕЗОПАСНОСТЬ" : "SAFETY";
        Lbl_SafetyDesc.Text = isRu
            ? "Рекомендуется создать точку восстановления перед применением твиков."
            : "It's recommended to create a restore point before applying tweaks.";
        Btn_RestorePoint.Content = isRu ? "Создать точку восст." : "Create Restore Point";
        Lbl_Monitoring.Text = isRu ? "МОНИТОРИНГ" : "MONITORING";
        Log_Line1.Text = isRu ? "[i] Фоновый анализ запущен..." : "[i] Background analysis running...";
        Log_Line2.Text = isRu ? "[+] Состояния тумблеров синхронизированы" : "[+] Toggle states synced";
        Log_Line3.Text = isRu ? "[+] Диск игры: D: (SSD)" : "[+] Game drive: D: (SSD)";

        // ===== Ресурсы =====
        Lbl_NetTest.Text = isRu ? "Тест сети" : "Network Test";
        Btn_RunTest.Content = isRu ? "Запустить тест" : "Run Test";
        Lbl_Ping.Text = isRu ? "ПИНГ" : "PING";
        Lbl_Ms.Text = isRu ? " мс" : " ms";
        Lbl_LowerBetter.Text = isRu ? "Меньше — лучше" : "Lower is better";
        Lbl_Download.Text = isRu ? "СКАЧИВАНИЕ" : "DOWNLOAD";
        Lbl_Upload.Text = isRu ? "ОТПРАВКА" : "UPLOAD";
        Lbl_MbpsDownload.Text = isRu ? " Мбит/с" : " Mbps";
        Lbl_MbpsUpload.Text = isRu ? " Мбит/с" : " Mbps";
        Lbl_MbpsNet.Text = isRu ? " Мбит/с" : " Mbps";
        Lbl_CpuMonitor.Text = "CPU Usage Monitor";
        Lbl_RamMonitor.Text = "Memory Usage Monitor";
        Lbl_DiskMonitor.Text = "Disk Activity Monitor";
        Lbl_NetMonitor.Text = "Network Usage Monitor";
        Lbl_AvgCpu.Text = isRu ? "Среднее 41%" : "Average 41%";
        Lbl_AvgRam.Text = isRu ? "Среднее 45%" : "Average 45%";
        Lbl_AvgDisk.Text = isRu ? "Среднее 19%" : "Average 19%";
        Lbl_AvgNet.Text = isRu ? "Среднее 58 Мбит/с" : "Average 58 Mbps";

        // Перерисовываем карточки твиков на 5 вкладках под новый язык
        PopulateTweakPages();

        // Обновляем сайдбар/топбар под текущую активную вкладку
        ApplyActiveNav(_currentTab);
    }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}

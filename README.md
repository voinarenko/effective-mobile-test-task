# Тестовое задание №2 для Effective Mobile
### Билд: [ссылка](https://drive.google.com/file/d/1DxxeH1crkTEZYYZd59KWfMf77Dt_z7BY/view?usp=sharing)
### Проект: [ссылка](https://drive.google.com/file/d/1vWEClWYahaZ7nTT__TDcYnMag5YtXIvp/view?usp=sharing)
### Общее время выполнения: 27 ч
## Управление:

&emsp;***W, A, S, D / Стрелки управления курсором*** — перемещение героя

&emsp;***Движение мышью*** — прицеливание

&emsp;***Клик левой кнопкой мыши*** — атака

&emsp;***Escape*** — выход

## Информация в HUD:

&emsp;***верхняя часть по центру*** — полоса здоровья

&emsp;***правый верхний угол 1-я линия*** — текущая волна

&emsp;***правый верхний угол 2-я линия*** — оставшееся количество врагов

&emsp;***левый верхний угол*** — таймер до начала волны

# Комментарий от проверяющего:
## Минусы:
# HeroShoot.OnDestroy вызывает DOTween.KillAll (рубит чужие твины) - 
# HeroMove двигает transform при наличии NavMeshAgent - ***исправлено***
# неиспользуемые OnDeath в Enemy/EnemyDeath - ***используются аним-ивентом***
# мёртвая _previousBlockType в LevelLoadState - ***исправлено***
# твины «пули» без ID/линков (висят при уничтожении хоста) - ***исправлено***
# RandomService.FillWeights не чистит список при повторном вызове, - ***вызывается только один раз, но исправлено***
# AssetProvider не проверяет null из Resources.Load - ***исправлено***

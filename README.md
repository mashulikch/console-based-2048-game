# Eng
# Console-based 2048 Game (C#)

A simple console implementation of the classic **2048** puzzle game written in **C#**.
Combine equal tiles to reach **2048** and get the highest score

## Features

* 4×4 board
* Random tiles (90% = 2, 10% = 4)
* Score tracking
* Controls: **W/A/S/D** or **Arrow keys**
* Win condition (tile **2048**) and game over detection
* Exit anytime with **Q** or **Enter**

## Requirements

* .NET SDK (recommended: **.NET 6+**)
* Windows / macOS / Linux (console)

## How to Run

### Option 1: Using Rider / Visual Studio

1. Open the project in your IDE
2. Run the project (`Program.cs`)

### Option 2: Using terminal

In the project folder:

```bash
dotnet run
```

## Controls

| Action     | Keys           |
| ---------- | -------------- |
| Move Up    | `W` or `↑`     |
| Move Down  | `S` or `↓`     |
| Move Left  | `A` or `←`     |
| Move Right | `D` or `→`     |
| Exit       | `Q` or `Enter` |

## Gameplay Rules

* Each move shifts all tiles in the chosen direction;
* Tiles with the same value merge into one tile with double value;
* After every successful move, a new tile (2 or 4) appears on an empty cell;
* You win when you create a **2048** tile;
* Game ends when no moves are possible.

## Project Structure

* `Program.cs` — game loop, board rendering, move logic, tile merging, score system.

---
# RU:  

# Консольная игра 2048 (C#)

Простая консольная версия классической головоломки **2048**, написанная на **C#**.
Нужно соединять одинаковые плитки, чтобы получить **2048** и набрать максимум очков

## Возможности

* Поле 4×4
* Случайные плитки (90% = 2, 10% = 4)
* Подсчёт очков
* Управление: **W/A/S/D** или **стрелки**
* Проверка победы (плитка **2048**) и завершения игры (нет ходов)
* Выход в любой момент по **Q** или **Enter**

## Требования

* .NET SDK (желательно: **.NET 6+**)
* Windows / macOS / Linux (консоль)

## Как запустить

### Вариант 1: Через Rider / Visual Studio

1. Открыть проект в IDE
2. Запустить проект (`Program.cs`)

### Вариант 2: Через терминал

В папке проекта:

```bash
dotnet run
```

## Управление

| Действие | Клавиши         |
| -------- | --------------- |
| Вверх    | `W` или `↑`     |
| Вниз     | `S` или `↓`     |
| Влево    | `A` или `←`     |
| Вправо   | `D` или `→`     |
| Выход    | `Q` или `Enter` |

## Правила игры

* За ход все плитки сдвигаются в выбранном направлении;
* Одинаковые плитки объединяются в одну с удвоенным значением;
* После успешного хода появляется новая плитка (2 или 4) в случайной пустой клетке;
* Победа — когда появляется плитка **2048**;
* Игра заканчивается, если нет доступных ходов.

## Структура проекта

* `Program.cs` — игровой цикл, отрисовка поля, логика ходов, объединение плиток, подсчёт очков.


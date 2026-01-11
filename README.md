# 🧩 Sudoku Solver – Forward Checking & MRV

This project is a **Sudoku solver** implemented in **C# (Windows Forms)** using **Backtracking combined with Forward Checking**, with optional **MRV (Minimum Remaining Values)** heuristic.

The solver treats Sudoku as a **Constraint Satisfaction Problem (CSP)**, where:
- each cell is a variable
- possible values form the domain
- row, column, and subgrid rules are constraints

The application supports **both 4×4 and 9×9 grids** and includes a visual step-by-step solving mode.

---

## 📸 Application Preview

### 4×4 Sudoku (Solved)
![4x4 Sudoku]("E:\CTI\An_IV\Semestrul_I\IA_IntelingentaArtificiala\Proiect_Sudoku\Sudoku_ForwardChecking\image2.png")

### 9×9 Sudoku (Solved)
![9x9 Sudoku]("E:\CTI\An_IV\Semestrul_I\IA_IntelingentaArtificiala\Proiect_Sudoku\Sudoku_ForwardChecking\image3.png")

---

## 🚀 Features

- Backtracking-based Sudoku solver  
- Forward Checking for domain pruning  
- Optional MRV heuristic to reduce search space  
- Supports **4×4** and **9×9** Sudoku grids  
- Step-by-step visualization (Slow Mode)  
- Execution time measurement  
- Interactive grid editing via keyboard  
- Reset and clear functionality  

---

## 🧠 Algorithm Overview

### Backtracking
The solver recursively assigns values to empty cells and backtracks when a constraint violation occurs.

### Forward Checking
After assigning a value, the solver:
- removes that value from the domains of all neighboring cells
- immediately backtracks if any domain becomes empty

This significantly reduces unnecessary exploration compared to naive backtracking.

### MRV (Minimum Remaining Values)
When enabled, the solver always selects the unassigned cell with the **smallest domain**, increasing efficiency on harder puzzles.

---

## 🖥️ User Interface

- Click a cell to select it  
- Use number keys to enter values  
- Backspace / Delete clears a cell  
- Toggle **Slow Mode** to visualize solving steps  
- Toggle **MRV** to compare performance  
- Timer displays total solving time  

---

## 🏗️ Project Structure

```
Sudoku/
├── Algoritm/
│   └── Solver.cs          # Backtracking + Forward Checking logic
├── Models/
│   ├── Grid.cs            # Sudoku grid representation
│   ├── Cell.cs            # Individual cell (CSP variable)
│   └── Coordinates.cs     # Row/column coordinates
├── SudokuForm.cs          # Windows Forms UI
```

---

## 📌 Notes

- The solver modifies the grid **in-place**
- A grid snapshot is stored before solving to allow reset
- The code is structured to allow future extensions:
  - AC-3 constraint propagation
  - puzzle generation
  - difficulty estimation
  - alternative heuristics

---

## 🔮 Possible Improvements

- Sudoku puzzle generator
- Difficulty classification
- Highlight fixed vs user-entered cells
- Performance benchmarking with/without MRV
- Separation of UI and logic (MVC-style)

---

## 🧠 Educational Purpose

This project serves as:
- a practical example of **CSP solving**
- a demonstration of **Forward Checking vs naive Backtracking**
- a reference implementation for AI / algorithms coursework

---

## 📄 License

This project is intended for educational purposes.

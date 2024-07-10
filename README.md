# Calculator Application

## Description

This project provides a calculator application with two modes of operation:

1. **Console Application Mode:**
    - Users interact with the calculator through the console.
    - Supports basic arithmetic operations with operator precedence (`* / + -`).
    - Handles expressions like `"2+2*3"` correctly with operator precedence.
    - Throws exceptions for division by zero (`"2/0"`).

2. **File Processing Mode:**
    - Reads input data from a file line by line.
    - Supports calculations with parentheses for precedence control.
    - Each line from the input file is evaluated, and results are written to an output file.
    - Outputs calculations and handles invalid expressions gracefully.

## Features

- **Console Application Mode:**
    - Allows users to input expressions directly in the console.
    - Supports basic arithmetic operations (`+ - * /`).
    - Handles operator precedence (`*` and `/` have higher precedence over `+` and `-`).
    - Throws exceptions for division by zero.

- **File Processing Mode:**
    - Reads expressions from an input file.
    - Evaluates expressions that may include parentheses for grouping.
    - Writes the evaluated results to an output file.
    - Handles and reports errors for invalid expressions.

### Examples

**Example 1: Console Application Mode**

Input:

`2+2*3`

Output:

`8`

Input:
`2/0
`

Output:

`Exception: Divide by zero
`

**Example 2: File Processing Mode**

Input file (`input.txt`):
```
1+2*(3+2)
1+x+4
2+15/3+4*2
```

Output file (`output.txt`):
```
1+2*(3+2) = 11
1+x+4 = Exception. Wrong input.
2+15/3+4*2 = 15```
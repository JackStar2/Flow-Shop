import itertools
import tkinter as tk
from tkinter import ttk
import matplotlib.pyplot as plt

# Function to calculate makespan
def calculate_makespan(jobs, processing_times):
    num_machines = len(processing_times[0])
    num_jobs = len(jobs)
    completion_times = [[0] * num_machines for _ in range(num_jobs)]
    start_times = [[0] * num_machines for _ in range(num_jobs)]  # To hold start times
    details = []  # To hold detailed calculations for display

    for j, job in enumerate(jobs):
        job_details = []  # Details for the current job
        for m in range(num_machines):
            if m == 0:
                start_times[j][m] = completion_times[j - 1][m] if j > 0 else 0
                completion_times[j][m] = start_times[j][m] + processing_times[job][m]
            else:
                start_times[j][m] = max(completion_times[j][m - 1], completion_times[j - 1][m] if j > 0 else 0)
                completion_times[j][m] = start_times[j][m] + processing_times[job][m]

            job_details.append(
                (job + 1, m + 1, start_times[j][m], completion_times[j][m]))  # Save job details (1-based indexing)

        details.append(job_details)  # Append job details for all machines

    makespan = completion_times[-1][-1]
    return makespan, details

def flow_shop_backtracking(jobs, processing_times):
    best_makespan = float('inf')
    best_sequence = None
    best_details = None

    def backtrack(current_sequence, current_makespan, current_details):
        nonlocal best_makespan, best_sequence, best_details

        if len(current_sequence) == len(jobs):
            if current_makespan < best_makespan:
                best_makespan = current_makespan
                best_sequence = current_sequence[:]
                best_details = current_details[:]
            return

        for job in jobs:
            if job not in current_sequence:
                new_sequence = current_sequence + [job]
                new_makespan, new_details = calculate_makespan(new_sequence, processing_times)
                if new_makespan < best_makespan:
                    backtrack(new_sequence, new_makespan, new_details)

    backtrack([], 0, [])
    return best_sequence, best_makespan, best_details

def calculate_orders():
    global best_sequence, best_makespan, best_details, original_order, original_makespan, jobs, processing_times
    if jobs is None or processing_times is None:
        return

    # Calculate the optimized order and its makespan
    best_sequence, best_makespan, best_details = flow_shop_backtracking(jobs, processing_times)

    # Calculate the makespan for the original order
    original_order = jobs[:]  # Copy the original job order
    original_makespan, _ = calculate_makespan(original_order, processing_times)

    # Clear the tree view
    for row in tree.get_children():
        tree.delete(row)

    # Insert original order and its makespan into the tree view
    tree.insert("", "end", values=("Original Order", [job + 1 for job in original_order], original_makespan))

    # Insert optimized order and its makespan into the tree view
    tree.insert("", "end", values=("Optimized Order", [job + 1 for job in best_sequence], best_makespan))

    optimized_order_str = ", ".join(str(job + 1) for job in best_sequence)
    optimized_order_label.config(text=f"Optimized Order: {optimized_order_str} | Makespan: {best_makespan}")

    # Show detailed calculations for the optimized order
    optimized_details_str = "Job | Machine | Start | Finish\n"  # Header
    optimized_details_str += "-" * 40 + "\n"  # Separator

    for i, details in enumerate(best_details):
        for j, m, start, finish in details:
            optimized_details_str += f"{j:<4} | {m:<7} | {start:<6} | {finish:<6}\n"
        optimized_details_str += "-" * 40 + "\n"  # Add a line after each job's details

    # Clear previous text and insert new details
    optimized_details_text.delete(1.0, tk.END)
    optimized_details_text.insert(tk.END, optimized_details_str)

def visualize_results():
    if best_sequence is None:
        return

    plot_flow_shop_chart([job + 1 for job in best_sequence])  # Plot with 1-based job IDs

def plot_flow_shop_chart(sequence):
    num_machines = len(processing_times[0])
    num_jobs = len(sequence)

    starts = [[0] * num_machines for _ in range(num_jobs)]
    completion_times = [[0] * num_machines for _ in range(num_jobs)]

    for j, job in enumerate(sequence):
        for m in range(num_machines):
            if m == 0:
                starts[j][m] = completion_times[j - 1][m] if j > 0 else 0
            else:
                starts[j][m] = max(completion_times[j][m - 1], completion_times[j - 1][m] if j > 0 else 0)

            completion_times[j][m] = starts[j][m] + processing_times[job - 1][m]

    plt.figure(figsize=(10, 6))
    job_colors = ['#FF0000', '#008000', '#008080', '#FFC0CB', '#A52A2A']  # Updated colors

    for j, job in enumerate(sequence):
        for m in range(num_machines):
            plt.barh(f'Machine {m + 1}', processing_times[job - 1][m], left=starts[j][m],
                     height=0.4, color=job_colors[j % len(job_colors)], edgecolor='black',
                     label=f'Job {job}' if m == 0 else "")

    plt.xlabel('Time')
    plt.ylabel('Machine ID')
    plt.title('Flow Shop Scheduling')
    plt.grid()
    plt.legend(loc='upper left', bbox_to_anchor=(1, 1))
    plt.tight_layout()
    plt.show()

# Initial job setup
jobs = [0, 1, 2]
processing_times = [
    [3, 7, 4], #Machine 1
    [5, 2, 6], #Machine 2
    [8, 4, 3], #Machine 3
]

# Tkinter GUI setup
root = tk.Tk()
root.title("Flow Shop Scheduling")

frame = tk.Frame(root)
frame.pack(pady=20)

# Treeview for displaying jobs
tree = ttk.Treeview(frame, columns=("Description", "Details", "Makespan"), show='headings')
tree.heading("Description", text="Description", anchor='center')
tree.heading("Details", text="Details", anchor='center')
tree.heading("Makespan", text="Makespan", anchor='center')

# Set the style for headers
style = ttk.Style()
style.configure("Treeview.Heading", font=("Helvetica", 10, "bold"))

# Center align columns
tree.column("Description", anchor='center')
tree.column("Details", anchor='center')
tree.column("Makespan", anchor='center')

tree.pack()

optimized_order_label = tk.Label(root, text="Optimized Order: ")
optimized_order_label.pack(pady=10)

# Text widget for optimized order details
optimized_details_text = tk.Text(root, height=10, width=60, wrap='word', font=("Helvetica", 10))
optimized_details_text.pack(pady=10)

# Button to calculate orders
calculate_button = tk.Button(root, text="Calculate Orders", command=calculate_orders)
calculate_button.pack(pady=10)

# Button to visualize the Gantt chart
visualize_button = tk.Button(root, text="Visualize Operations", command=visualize_results)
visualize_button.pack(pady=10)

root.mainloop()
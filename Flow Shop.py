import numpy as np

jobs = [0, 1, 2]

processing_times = [
    [3, 7, 4], #Machine 1
    [5, 2, 6], #Machine 2
    [8, 4, 3], #Machine 3
]

def calculate_total(jobs, processing_times):
    num_machine = len(processing_times)
    num_job = len(jobs)
    compleition_time = np.full(([num_job,num_machine]),0, dtype=int)

    for j, job in enumerate(jobs):
        for m in range(num_machine):
            if j == 0:
                compleition_time[j][m] = processing_times[job][m] if m == 0 else compleition_time[j][m-1] + processing_times[job][m]
            else:
                compleition_time[j][m] = compleition_time[j-1][m] + processing_times[job][m] if m == 0 \
                    else max(compleition_time[j-1][m-1], compleition_time[j-1][m]) + processing_times[job][m]

    total_time = compleition_time[-1][-1]
    return total_time


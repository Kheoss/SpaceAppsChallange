from math import *

class CArray:
    def __init__(self, *arr):
        self.internal = arr
        self.length = len(self.internal)

    def get(self, index):
        return ((index-floor(index))*self.internal[ceil(index)]+(1-(index-floor(index)))*self.internal[floor(index)] if floor(index)<index else self.internal[index])

    def add(self):
        s=0
        for i in self.internal:
            s+=i
        return s

    def insert(self, value, index):
        self.internal = self.internal[:index] + [value] + self,internal[index:]

    

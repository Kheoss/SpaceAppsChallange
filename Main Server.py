import numpy
from math import *
from PyFiles.clas import *
from numpy.random import random as rn
from flask import Flask, request, jsonify

app = Flask("app")

@app.route("/", methods=['POST'])
def result():
    
    garr = CArray()
    bias = rn()
    
    x = request.values.get('lifex')
    y = request.values.get('lifey')
    lifeformx = request.values.get('lifeformx')
    lifeformy = request.values.get('lifeformy')
    nbx = request.values.get('nbx')
    nby = request.values.get('nby')
    n0x = request.values.get('n0x')
    n0y = request.values.get('n0y')
    fcx = request.values.get('fcx')
    fcy = request.values.get('fcy')
    pax = request.values.get('pax')
    pay = request.values.get('pay')

    

    resultlifeform = [lifeformx, lifeformy][int(bias)]

    garr = CArray(nbx, nby)
    nb = garr.get(bias)

    garr = CArray(n0x, n0y)
    n0 = garr.get(bias)

    garr = CArray(fcx, fcy)
    fc = garr.get(bias)

    garr = CArray(x, y)
    resultlife = garr.get(bias)

    garr = CArray(pax, pay)
    pa = garr.get(bias)

    formula = lambda a,b,c,d : a*c*d/b

    timeFormula = lambda x,a,b,c,d : formula(a,b,c,d)*x

    resultingTime = float(0)

    while(timeFormula(resultingTime,nb,n0,fc,pa)<0.5):
        resultingTime+=0.01

    arr = [int("1") for i in range(int(resultlife))]+[int("0") for i in range(100-int(resultlife))]
    populated = False
    if numpy.random.choice(arr):
        populated = True

    data = {"Nb":nb, "N0":n0, "Fc":fc, "Pa":pa, "Time":resultingTime, "LifeProb":resultlife, "LifeForm":resultlifeform, "LifeExpect":formula(nb, n0, fc, pa), "Populated":populated}

    return jsonify(data)

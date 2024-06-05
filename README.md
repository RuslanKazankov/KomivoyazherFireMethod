# Входные параметры 2 файла:
## KomiData.txt
Файл с квадратной матрицей длин путей между городами
### Например
```
0 15 46 32
15 0 12 63
46 12 0 11
32 63 11 0
```
## KomiParams.txt
### Примеры
#### Экспоненциальное охлаждение
```
startTemperature=10000
endTemperature=1
coolingRate=0.003
limitIterations=5000
coolingFunction=ExponentialCooling
```
#### Геометрическое охлаждение
```
startTemperature=10000
endTemperature=1
coolingRate=0.9986
limitIterations=5000
coolingFunction=GeometricCooling
```

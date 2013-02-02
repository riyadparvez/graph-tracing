graph-tracing
=============

This tool traces different curves in a graph and outputs values of curves based on registered coordinates. You can export the data in CSV format
It uses simple noise elimination. Given a threshold this toll removes any pixel below this threshold making the image in binary two dimensional array. Then use connected component alogrithm to extract feature from images. 

Use 
> GraphTracing.exe "path of image"

![Sample Image](http://i.imgur.com/4q2feNJ.png)
Above image has five features. Output of the tool is:

> Component Count: 5
> ----------------------------------------------
> Componenets:
> ----------------------------------------------
> 
> 22 170
> 22 171
> 22 172
> 23 169
> 23 170
> 23 171
> 23 172
> 23 173
> 23 174
> 24 169
> 24 170
> 24 171
> 24 172
> 24 173
> 24 174
> 24 175
> 24 176
> 25 169
> 25 170
> 25 171
> 25 172
> 25 173
> 25 174
> 25 175
> 25 176
> 25 177
> More
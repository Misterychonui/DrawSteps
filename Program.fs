open System.Windows.Forms
open System.Drawing
open System

let pen = new Pen(Color.Black)
let brush = new SolidBrush(Color.Red)

let pt1 = new Point(x=0,y=600)
let pt2 = new Point(x=300,y=100)
let pt3 = new Point(x=600,y=600)


let rec BuildSerpinsky n f (pt1:Point) (pt2:Point) (pt3:Point) =
       if n<>0
       then
            f (pt1) (pt2) 
            f (pt2) (pt3)
            f (pt3) (pt1)
            let p12 = new Point(pt1.X + (pt2.X - pt1.X) / 2, pt1.Y + (pt2.Y - pt1.Y) / 2)
            let p23 = new Point(pt2.X + (pt3.X - pt2.X) / 2, pt2.Y + (pt3.Y - pt2.Y) / 2)
            let p13 = new Point(pt1.X + (pt3.X - pt1.X) / 2, pt1.Y + (pt3.Y - pt1.Y) / 2)
            BuildSerpinsky (n-1) f (pt1) (p12) (p13)
            BuildSerpinsky (n-1) f (p12) (pt2) (p23)
            BuildSerpinsky (n-1) f (p13) (p23) (pt3)

let form =
    let W,H = 800,800
    let b = new Bitmap(W,H)
    let g = Graphics.FromImage(b)
    BuildSerpinsky 3 (fun x y -> g.DrawLine(pen, x, y)) (pt1) (pt2) (pt3)  
    let temp = new Form()
    temp.Height <-H
    temp.Width<-W
    temp.Paint.Add(fun e -> e.Graphics.DrawImage(b, 0, 0))
    temp
[<STAThread>]
do Application.Run(form);;
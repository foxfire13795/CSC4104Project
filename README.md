# CSC4101Projects

11-13-2016: Project2 created

Comment 11-13-2016 2:53AM:
Have spent the evening constructing eval methods. Both If and Define are working well, but my testing of Cond is resulting in many errors. I've been using the test function:
(define zeroness
  (lambda (x)
    (cond
      ((b= 0 x) 'zero)
      (#t 'nonzero))))
Every time I go through the code, I get stuck on the return value. Trying the simpler function: (cond ((b= 5 5) 1) (#t 0)) resulted in the correct answer only when apply was removed from the function call. When apply is activated, the return value is always null. Idk.

Comment 11-12-2016 5:19AM:
Your code is looking fantastic so far; it appears to be working perfectly. I'm still battling the eval methods, but it seems as though you're done for now. I'll send you questions about your parts of the code if I run into issues when I get working on it again...a little later in the day lol. Just wanted to put this out here since I don't want to text you at such an odd time. 
If you're wondering about my progress, well, it's quite a bit slower. The problem here is writing original methods that all coordinate properly, based on the scheme interpreter written in scheme. I keep running into small quirks (like how cdr is a node/subtree in C# but a list in Scheme48) that keep messing with my implementation of the code in C#. 

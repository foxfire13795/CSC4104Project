;;; A Scheme Interpreter

;; eval takes an expression and an environment, checks the type of the
;; expression and evaluates it appropriately.  In your C++/Java
;; implementation, you would implement each branch of the cond as a
;; virtual function in the appropriate subclass of class Node or
;; in the appropriate subclass of class Special.
(define (eval exp env)
  (cond ((symbol? exp) (lookup exp env))		; variable      ;;if       exp isSymbol, return lookup exp
	((not (pair? exp)) exp)				; constant              ;;else if !exp,isPair, return exp
	((eq? (car exp) 'closure) exp)			; closure           ;;else if exp.car = closure, return exp
	((eq? (car exp) 'quote) (cadr exp))		; quote             ;;else if exp.car = quote, return exp.car.cdr
	((eq? (car exp) 'lambda)			; lambda                ;;else if exp.car = lambda,
	 (list 'closure exp env))                                   ;;	-return '(closure, exp, env)
	((eq? (car exp) 'begin)				; begin                 ;;else if exp.car = begin,
	 (evalbody (cdr exp) env))                                  ;;	-evalbody(exp.cdr)
	((eq? (car exp) 'if)				; if                    ;;else if exp.car = if,
	 (if (eval (cadr exp) env)                                  ;;	-if eval(exp.car.cdr) != null,
	     (eval (caddr exp) env)                                 ;;		-eval(exp.car.cdr.cdr)
	     (eval (cadddr exp) env)))                              ;;	-else, eval(exp.car.cdr.cdr.cdr)
	((eq? (car exp) 'let) (eval-let exp env))	; let           ;;else if exp.car = let, return evallet(exp)
	((eq? (car exp) 'cond) (eval-cond exp env))	; cond          ;;else if exp.car = cond, return evalcond(exp)
	((eq? (car exp) 'define) (eval-define exp env))	; define    ;;else if exp.car = define, return evaldefine(exp)
	((eq? (car exp) 'set!) (eval-set! exp env))	; set!          ;;else if exp.car = set, return evalset(exp)
	(else (let ((call (evallist exp env)))		; apply         ;;else, ??? a bit confused here 
		(apply (car call) (cdr call))))                         ;;
	))

;; Make sure you understand how the lookup function traverses the
;; environment.
;; The best way to implement lookup is to define a class Environement
;; and to make lookup() a method of that class.
(define (lookup exp env)
  (if (null? env)
      ; If we didn't find it in env, look in the scheme48 environment.
      ; The interpreter writtin in C++/Java must implement builtin-eval.
      (builtin-eval exp (interaction-environment))
      (let ((binding (assq exp (car env))))
	(if (pair? binding)
	    (cadr binding)
	    (lookup exp (cdr env))))))

;; Evaluate a list of expressions and return the list of results.
(define (evallist l env)										;;declare evallist(list, env)
  (map (lambda (x) (eval x env)) l))							;;evaluate each exp in list (right?)

;; Evaluate a list of expressions and return the result of the last one.
;; This is inefficient but it works.
(define (evalbody l env)										;;declare function evalbody(list, env)
  (car (reverse (evallist l env))))								;;return the last value in the evallist

;; Evaluate the expressions in the let-bindings, put a new scope in
;; front of env and use evalbody to evaluate the body.
;; Evaluating the expressions in the bindings can be done quite
;; elegantly using map.
(define (eval-let exp env)														;;declare function evallet
  (let ((let-env (cons (map (lambda (x) (list (car x) (eval (cadr x) env)))		;;	-let-env = '(x.car, eval(x.car.cdr)) where x = exp.car.cdr
			    (cadr exp))														;;	-**let-env = '(exp.car.car.cdr, eval(exp.car.cdr.car.cdr), env)	
		       env)))															;;	
    (evalbody (cddr exp) let-env)))												;;	-evalbody(exp.cdr.cdr, env: let-env)

;; eval-cond is implemented here as a single function, but it might be
;; easier to use a local helper function for evaluating a single case.
(define (eval-cond exp env)
  (cond ((null? exp) '())                               		;;if exp is ever null, return nil node, else, go on
	((eq? (car exp) 'cond) (eval-cond (cdr exp) env))   		;;    -if 1st exp(car) is cond, eval cond - recursion
	(else (let ((test (caar exp))                       		;;else, caar = test var
		    (body (cdar exp)))                          		;;    -body = cdar
		(if (eq? test 'else)                            		;;    -if test = else          
		    (if (null? body) '() (evalbody body env))   		;;    	-if body is null, return nil and eval body
		    (let ((val (eval test env)))                		;;        -val = eval(test))
		      (if val                                   		;;        -if val != null
			  (if (null? body) val (evalbody body env)) 		;;        	-if body != null, return val and eval body
			  (eval-cond (cdr exp) env))))))))          		;;        -else, evalcond(cdr exp)  

;; Construct a lambda expression for function definitions and call
;; eval recursively to evaluate the RHS of the definition.
(define (eval-define exp env)                                   ;;declare evaldefine
  ;; The local helper function def! adds a binding to the environment
  ;; as explained on the handout.
  ;; The best way to implement def! is as a method of a class Environment.
  (define (def! name value)                                     ;;	-declare helper def! as function with name and value
    (let ((binding (assq name (car env))))                      ;;	-Node binding = pair of find name in car of env 
      (if (pair? binding)                                       ;;	-if binding is a pair and not null
	  (set-cdr! binding (list value))                           ;;		-set the cdr of binding to value(a list)
	  (set-car! env (cons (list name value) (car env))))))      ;;	else 
	                                                            ;;		-set car of env to '(name, value, (car env))

  (if (symbol? (cadr exp))                                      ;;if exp.getcar.getcdr is symbol
      (def! (cadr exp) (eval (caddr exp) env))                  ;;	-exp.getcar.setcdr = eval(exp.car.cdr.cdr)
      (let ((name (caadr exp))                                  ;;else, name = exp.car.car.cdr
	    (parm (cdadr exp))                                      ;;	-parm = exp.cdr.car.cdr
	    (body (cddr exp)))                                      ;;    -body = exp.cdr.cdr
	(let ((value (eval (cons 'lambda (cons parm body)) env)))   ;;    ???-value = eval('lambda, parm, body)
	  (def! name value)))))

;; Find the definition of the variable if it exists and assign the value.
(define (eval-set! exp env)                            			;;declare evalset
  ;; The local helper function lookup is similar to lookup for assigning
  ;; to a variable.  Alternatively, one could modify lookup to return the
  ;; binding, use it here, and change the case for symbols in eval.
  ;; The best way to implement lookup is as a method of a class Environment.
  (define (lookup name env)                            			;;declare helper function lookup(name, env)
    (if (null? env)                                    			;;	-if env is null,
	'()                                                			;;		-return Nil
	(let ((binding (assq name (car env))))             			;;	-else, binding = find name in car of env
	  (if (pair? binding)                              			;;		-if binding isPair
	      binding                                      			;;			-binding = lookup(name, cdr of env)
	      (lookup name (cdr env))))))                  
   
  (let ((binding (lookup (cadr exp) env)))             			;;binding = lookup(exp.car.cdr)
    (if (pair? binding)                                			;;if binding isPair
	(set-cdr! binding (list (eval (caddr exp) env)))   			;;	-set cdr of binding to eval(exp.car.cdr.cdr)
	binding)))                                         			;;else, return binding 

;; The first argument of apply is either a scheme48 closure, in which
;; case the built-in function procedure? returns #t, or a closure of
;; the form (closure (lambda ...) env).
;; The easiest way to implement apply is as a virtual function in the
;; Node and Special hierarchy, which reports an error for all classes
;; other than Regular.
(define (apply f args)
  ;; A helper function for pairing up parameters and arguments.
  ;; Instead of proper error handling if the lists are not the same length,
  ;; extra arguments in l2 are ignored, missing arguments default to '().
  (define (pairup l1 l2)
    (cond ((null? l1) '())
	  ((null? l2) (cons (list (car l1) '()) (pairup (cdr l1) '())))
	  (else (cons (list (car l1) (car l2)) (pairup (cdr l1) (cdr l2))))))

  ; Call builtin-apply from top-level.scm for built-in functions.
  (cond ((procedure? f) (builtin-apply f args))
	; If it's not a closure this is an error.  No proper error handling.
	((not (and (pair? f) (eq? (car f) 'closure))) '())
	; Now we got a closure, take it apart and call evalbody.
	(else (let ((fun (cadr f))
		    (env (caddr f)))
		(let ((parm (cadr fun))
		      (body (cddr fun)))
		  (evalbody body (cons (pairup parm args) env)))))))

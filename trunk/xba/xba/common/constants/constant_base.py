class _ConstantMeta(type):
    def __new__(cls, name, bases, attrs):
        super_new = super(_ConstantMeta, cls).__new__
        parents = [b for b in bases if isinstance(b, _ConstantMeta)]
        if not parents:
            return super_new(cls, name, bases, attrs)
        module = attrs.pop('__module__')
        cls.__changable = True
        new_class = super_new(cls, name, bases, {'__module__': module})
        all = set()
        for attr, value in attrs.iteritems():
            all.add(value)
            setattr(new_class, attr, value)
        setattr(new_class, "ALL_CONST", all)
        setattr(new_class, "__init__", new_class._init)
        cls.__changable = False
        return new_class
    
    def _init(self, *args, **kwargs):
        msg = "constants can't be initialize"
        raise msg
    
    def __setattr__(self, name, value):
        if not self.__changable:
            msg = "constants can't be changed"
            raise msg
        else:
            super(_ConstantMeta, self).__setattr__(name, value)

class ConstantBase(object):
    __metaclass__ = _ConstantMeta
    
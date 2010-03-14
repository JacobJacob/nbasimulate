#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

from django import template

from gba.common.constants import oten_color_map
from gba.common.constants import TacticalSectionTypeMap

register = template.Library()

@register.filter
def check_attr(attr_oten):
    for map_info in oten_color_map:
        if attr_oten >= map_info[0]:
            return map_info[1]
    return 0

@register.filter
def section_name(section):
    return TacticalSectionTypeMap.get(section)

#class ExtendVariable(template.Variable):
#    r"""
#    A template variable, resolvable against a given context. The variable may be
#    a hard-coded string (if it begins and ends with single or double quote
#    marks)::
#
#        >>> c = {'article': {'section':u'News'}}
#        >>> Variable('article.section').resolve(c)
#        u'News'
#        >>> Variable('article').resolve(c)
#        {'section': u'News'}
#        >>> class AClass: pass
#        >>> c = AClass()
#        >>> c.article = AClass()
#        >>> c.article.section = u'News'
#
#    (The example assumes VARIABLE_ATTRIBUTE_SEPARATOR is '.')
#    """
#
#    def __init__(self, var):
#        super(ExtendVariable, self).__init__(var)
#
#    def resolve(self, context):
#        """Resolve this variable against a given context."""
#        if self.lookups is not None:
#            # We're dealing with a variable that needs to be resolved
#            #先做一下处理,如果lookup是以结尾，则先用lookup[-1:]查找该值再往下查找
#            temp_lookups = []
#            for r in self.lookups:
#                if r.endswith('_'):
#                    r = r[-1:]
#                    self.lookups = [r]
#                    r = self._resolve_lookup(context)
#                temp_lookups.append(r)
#                
#            self.lookups = temp_lookups
#                
#            value = self._resolve_lookup(context)
#        else:
#            # We're dealing with a literal, so it's already been "resolved"
#            value = self.literal
#        if self.translate:
#            return _(value)
#        return value
#
#    def __repr__(self):
#        return "<%s: %r>" % (self.__class__.__name__, self.var)
#
#    def __str__(self):
#        return self.var
#
#    def _resolve_lookup(self, context):
#        """
#        Performs resolution of a real variable (i.e. not a literal) against the
#        given context.
#
#        As indicated by the method's name, this method is an implementation
#        detail and shouldn't be called by external code. Use Variable.resolve()
#        instead.
#        """
#        current = context
#        print self.lookups
#        for bit in self.lookups:
#            try: # dictionary lookup
#                current = current[bit]
#            except (TypeError, AttributeError, KeyError):
#                try: # attribute lookup
#                    current = getattr(current, bit)
#                    if callable(current):
#                        if getattr(current, 'alters_data', False):
#                            current = settings.TEMPLATE_STRING_IF_INVALID
#                        else:
#                            try: # method call (assuming no args required)
#                                current = current()
#                            except TypeError: # arguments *were* required
#                                # GOTCHA: This will also catch any TypeError
#                                # raised in the function itself.
#                                current = settings.TEMPLATE_STRING_IF_INVALID # invalid method call
#                            except Exception, e:
#                                if getattr(e, 'silent_variable_failure', False):
#                                    current = settings.TEMPLATE_STRING_IF_INVALID
#                                else:
#                                    raise
#                except (TypeError, AttributeError):
#                    try: # list-index lookup
#                        current = current[int(bit)]
#                    except (IndexError, # list index out of range
#                            ValueError, # invalid literal for int()
#                            KeyError,   # current is a dict without `int(bit)` key
#                            TypeError,  # unsubscriptable object
#                            ):
#                        raise template.VariableDoesNotExist("Failed lookup for key [%s] in %r", (bit, current)) # missing attribute
#                except Exception, e:
#                    if getattr(e, 'silent_variable_failure', False):
#                        current = settings.TEMPLATE_STRING_IF_INVALID
#                    else:
#                        raise
#        return current
#
#class ExtendIfEqualNode(template.Node):
#    def __init__(self, var1, var2, nodelist_true, nodelist_false, negate):
#        self.var1, self.var2 = ExtendVariable(var1), ExtendVariable(var2)
#        self.nodelist_true, self.nodelist_false = nodelist_true, nodelist_false
#        self.negate = negate
#
#    def __repr__(self):
#        return "<IfEqualNode>"
#
#    def render(self, context):
#        try:
#            val1 = self.var1.resolve(context)
#        except template.VariableDoesNotExist:
#            val1 = None
#        try:
#            val2 = self.var2.resolve(context)
#        except template.VariableDoesNotExist:
#            val2 = None
#        if (self.negate and val1 != val2) or (not self.negate and val1 == val2):
#            return self.nodelist_true.render(context)
#        return self.nodelist_false.render(context)
#    
#def do_extendifequal(parser, token, negate):
#    bits = list(token.split_contents())
#    if len(bits) != 3:
#        raise template.TemplateSyntaxError, "%r takes two arguments" % bits[0]
#    end_tag = 'end' + bits[0]
#    nodelist_true = parser.parse(('else', end_tag))
#    token = parser.next_token()
#    if token.contents == 'else':
#        nodelist_false = parser.parse((end_tag,))
#        parser.delete_first_token()
#    else:
#        nodelist_false = template.NodeList()
#        
#    #print bits[1], bits[2], nodelist_true, nodelist_false, negate
#        
#    return ExtendIfEqualNode(bits[1], bits[2], nodelist_true, nodelist_false, negate)
#
##@register.tag
#def extendifequal(parser, token):
#    """"""
#    return do_extendifequal(parser, token, False)
#
#ifequal = register.tag(extendifequal)


